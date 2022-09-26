using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Buildings;
using UniRx;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerClaw : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _claw;
        [SerializeField] private SpriteRenderer _clawSprite;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Transform _place;
        [SerializeField] private Transform _stickingTransform;
        [SerializeField] private float _lerpSpeed = 1f;
        [SerializeField] private LayerMask _buildingLayer;

        private bool _released;
        private IDisposable _stickDisposable;

        public async UniTask<bool> Release()
        {
            _collider2D.enabled = true;
            _clawSprite.enabled = true;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, -transform.up, 100, _buildingLayer);

            if (raycastHit2D)
            {
                await transform.DOMove(raycastHit2D.point, 1).AsyncWaitForCompletion();

                return true;
            }

            return false;
        }

        private void Stick(Vector3 stickPos)
        {
            _stickDisposable = Observable.EveryUpdate().Subscribe((l => { _stickingTransform.position = stickPos; }));
        }

        public void StopSticking()
        {
            _stickDisposable?.Dispose();
        }

        public async UniTask PullBack()
        {
            StopSticking();

            var distance = (_place.position - transform.position).sqrMagnitude;

            while (distance > 0.01f)
            {
                transform.localPosition =
                    Vector3.Lerp(transform.localPosition, Vector3.zero, _lerpSpeed * Time.deltaTime);

                distance = (_place.position - transform.position).sqrMagnitude;
                await UniTask.Yield();
            }

            _released = false;
            // transform.position = _place.position;

            // _collider2D.enabled = false;
            // _clawSprite.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Building>(out var building))
            {
                _released = true;

                Vector2 closestPoint = other.ClosestPoint(transform.position);

                Stick(closestPoint);
            }
            else if (other.gameObject.TryGetComponent<Planet>(out var planet))
            {
                _released = true;
                PullBack();
            }
        }
    }
}