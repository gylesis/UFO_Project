using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Project.AI
{
    public class AntiAirDefenseMissile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private SpriteRenderer _view;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private CollisionEvent _collisionEvent;

        private Vector2 _forceDirection;
        private Transform _target;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public Subject<Collision2D> CollisionEnter => _collisionEvent.CollisionEnter;

        private bool _allowedToMove = true;

        public void Setup(Vector2 forceDirection)
        {
            _forceDirection = forceDirection;
        }

        public void Setup(Transform target)
        {
            _target = target;
        }

        public void OnExplode()
        {
            _allowedToMove = false;
            _view.DOFade(0, 0.5f).OnComplete((() => Destroy(gameObject)));
        }

        private void Update()
        {
            if (_allowedToMove == false) return;

            Vector3 rotateDirection;

            Vector3 moveDirection;

            if (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;

                moveDirection = direction;
                rotateDirection = direction;
            }
            else
            {
                moveDirection = _forceDirection;
                rotateDirection = _forceDirection;
            }

            _rigidbody.velocity = moveDirection * _speed * Time.deltaTime;
            transform.up = rotateDirection;
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }
    }

    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
    }
}