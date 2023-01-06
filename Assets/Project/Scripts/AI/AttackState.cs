using System;
using System.Collections.Generic;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.AI
{
    public class AttackState : IState
    {
        private readonly PlayerHealthService _playerHealthService;
        private AntiAirDefense _airDefense;
        private Transform _target;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _collisionDisposable = new CompositeDisposable();

        private AntiAirDefenseMissile _antiAirDefenseMissile;
        private FXManager _fxManager;


        private Dictionary<AntiAirDefenseMissile, IDisposable> _collisionDictionary =
            new Dictionary<AntiAirDefenseMissile, IDisposable>();

        public AttackState(AntiAirDefense airDefense, PlayerContainer playerContainer,
            AntiAirDefenseMissile antiAirDefenseMissile, FXManager fxManager, PlayerHealthService playerHealthService)
        {
            _playerHealthService = playerHealthService;
            _fxManager = fxManager;
            _antiAirDefenseMissile = antiAirDefenseMissile;
            _target = playerContainer.Transform;
            _airDefense = airDefense;
        }

        public void Enter()
        {
            _compositeDisposable = new CompositeDisposable();
            _airDefense.SpriteRenderer.color = Color.red;

            LookAtTarget();

            SpawnRockets();
        }

        private void SpawnRockets()
        {
            Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe((l => { ShootTarget(); }))
                .AddTo(_compositeDisposable);
        }

        private void ShootTarget()
        {
            AntiAirDefenseMissile missile = Object.Instantiate(_antiAirDefenseMissile,
                _airDefense.Krutilka.position, Quaternion.identity);

            missile.Setup(_target);

            IDisposable disposable = missile.CollisionEnter.Subscribe((collision =>
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    _playerHealthService.ApplyDamage(1);

                    Debug.Log($"Player hit by missile!");

                    _collisionDictionary[missile].Dispose();
                    _collisionDictionary.Remove(missile);

                    Vector2 pos = collision.contacts[0].point;
                    _fxManager.MissileExplosion(pos);
                    missile.OnExplode();
                }
            }));

            _collisionDictionary.Add(missile, disposable);

            Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe((_ =>
            {
                if (missile == null) return;

                _collisionDictionary[missile].Dispose();
                _collisionDictionary.Remove(missile);

                missile.OnExplode();
                _fxManager.MissileExplosion(missile.transform.position);
            }));
        }

        private void LookAtTarget()
        {
            Observable.EveryUpdate().Subscribe((l =>
            {
                var direction = (_target.position - _airDefense.transform.position).normalized;
                _airDefense.Krutilka.right = direction;
            })).AddTo(_compositeDisposable);
        }

        public void Exit()
        {
            _airDefense.SpriteRenderer.color = Color.white;
            _compositeDisposable?.Dispose();
            _collisionDisposable?.Dispose();
        }
    }
}