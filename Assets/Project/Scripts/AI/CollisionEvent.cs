using UniRx;
using UnityEngine;

namespace Project.AI
{
    public class CollisionEvent : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        // [SerializeField] private LayerMask _collisionLayers;  

        public Subject<Collision2D> CollisionEnter { get; } = new Subject<Collision2D>();

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEnter.OnNext(other);
        }
    }
}