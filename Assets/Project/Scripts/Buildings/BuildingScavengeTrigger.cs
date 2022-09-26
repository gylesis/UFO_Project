using System;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Buildings
{
    public class BuildingScavengeTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D _triggerCollider2D;
        [SerializeField] private Transform _scavengePoint;

        public Transform ScavengePoint => _scavengePoint;

        private Building _building;
        private IDisposable _disableDisposable;

        public Subject<Building> ScavengeStart { get; } = new Subject<Building>();
        public Subject<Building> Exit { get; } = new Subject<Building>();

        [Inject]
        private void Init(Building building)
        {
            _triggerCollider2D.isTrigger = true;
            _building = building;
        }

        public void Activate()
        {
            _triggerCollider2D.enabled = true;
        }

        public void Disable()
        {
            _triggerCollider2D.enabled = false;
        }

        public void Disable(TimeSpan time)
        {
            _disableDisposable?.Dispose();

            _disableDisposable = Observable.Timer(time).Subscribe((_ => { _triggerCollider2D.enabled = false; }));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var isPlayer = other.TryGetComponent<PlayerController>(out var playerController);

            if (isPlayer)
                ScavengeStart.OnNext(_building);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var isPlayer = other.TryGetComponent<PlayerController>(out var playerController);

            if (isPlayer)
                Exit.OnNext(_building);
        }
    }
}