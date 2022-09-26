using System;
using DG.Tweening;
using Project.AI;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerController : MyMonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 0.1f;
        [SerializeField] private float _lerpRotate = 0.1f;
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _speedX = 5f;
        [SerializeField] private float _speedY = 5f;

        [SerializeField] private float _krutilka = 1;


        private HeightRestrictService _heightRestrictService;

        private InputService _inputService;
        private Vector3 _movePos;
        public bool IsStickingLocked { get; set; }

        [Inject]
        private void Init(HeightRestrictService heightRestrictService,
            InputService inputService)
        {
            _inputService = inputService;
            _heightRestrictService = heightRestrictService;

            _movePos = transform.position;
        }

        private void Update()
        {
            if (IsStickingLocked) return;

            Rotation();

            Move();
            // Move2();
        }

        public void SetXSpeed(float speed)
        {
            DOVirtual.Float(_speedX, speed, 0.5f, (value => _speedX = value));
        }

        public void SetYSpeed(float speed)
        {
            DOVirtual.Float(_speedY, speed, 0.5f, (value => _speedY = value));
        }

        private void Rotation()
        {
            Vector2 radiusVector = CoordinatesService.GetRadiusVector(transform.position);

            radiusVector.Normalize();

            transform.up = Vector3.Lerp(transform.up, (Vector3) radiusVector, _lerpRotate);
        }

        public void Move()
        {
            Vector2 radiusVector = CoordinatesService.GetRadiusVector(transform.position);
            Vector3 direction = Vector3.Cross(radiusVector.normalized, Vector3.forward * _inputService.TouchDelta.x);

            Vector3 movePos = transform.position + direction * _speedX;

            var radius = CoordinatesService.GetRadius(transform.position);

            radius += _inputService.TouchDelta.y * _speedY;

            radius = Mathf.Clamp(radius, _heightRestrictService.MinHeight, _heightRestrictService.MaxHeight);

            var angle = CoordinatesService.GetAngleDeg(movePos);

            var polarVector = new PolarVector(radius, angle);

            Vector2 polarCoords = CoordinatesService.GetPolarCoords(polarVector);

            transform.position = polarCoords;
        }

        public void Move2()
        {
            if (_inputService.TouchDelta.magnitude != 0)
            {
                Vector2 radiusVector = CoordinatesService.GetRadiusVector(_movePos);
                Vector3 direction = Vector3.Cross(radiusVector.normalized,
                    Vector3.forward * _inputService.TouchDelta.normalized.x);

                _movePos = transform.position + direction * _krutilka;
            }

            Vector3 gg = transform.position + (_movePos - transform.position).normalized;

            var radius = CoordinatesService.GetRadius(_movePos);

            radius += _inputService.TouchDelta.y * _speedY;

            radius = Mathf.Clamp(radius, _heightRestrictService.MinHeight, _heightRestrictService.MaxHeight);

            var angle = CoordinatesService.GetAngleDeg(_movePos);

            var polarVector = new PolarVector(radius, angle);

            Vector2 polarCoords = CoordinatesService.GetPolarCoords(polarVector);

            transform.position = Vector2.Lerp(transform.position, polarCoords, _lerpSpeed);
        }

        public void StartLeprTo(Transform target, Action onLerpEnded)
        {
            IsStickingLocked = true;

            _rigidbody.DOMove(target.position, 1).SetEase(Ease.Linear).OnComplete((onLerpEnded.Invoke));
        }

        public void StopLerp()
        {
            IsStickingLocked = false;
        }

        /*private void OnDrawGizmos()
        {
            if (_inputService.TouchDelta.magnitude != 0)
            {
                Vector2 radiusVector = CoordinatesService.GetRadiusVector(_movePos);
                Vector3 direction = Vector3.Cross(radiusVector.normalized, Vector3.forward * _inputService.TouchDelta.normalized.x);

                _movePos = transform.position + direction * _krutilka;
                
                Gizmos.DrawLine(transform.position, _movePos);
            }

            Vector3 gg = transform.position + (_movePos - transform.position).normalized;
            
            var radius = CoordinatesService.GetRadius(_movePos);

            radius += _inputService.TouchDelta.y * _speedY;

            radius = Mathf.Clamp(radius, _heightRestrictService.MinHeight, _heightRestrictService.MaxHeight);

            var angle = CoordinatesService.GetAngleDeg(_movePos);

            var polarVector = new PolarVector(radius, angle);

            Vector2 polarCoords = CoordinatesService.GetPolarCoords(polarVector);
            
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(gg,0.2f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_movePos,0.2f);

        }*/
    }

    public class MyMonoBehaviour : MonoBehaviour
    {
        protected CoordinatesService CoordinatesService;

        [Inject]
        private void Init(CoordinatesService coordinatesService)
        {
            CoordinatesService = coordinatesService;
        }
    }
}