using UniRx;
using UnityEngine;

namespace Project.Scripts
{
    public class RestrictionalCircle : MonoBehaviour
    {
        [Range(1,3)][SerializeField] private int _heightLevel;

        public int HeightLevel => _heightLevel;

        public Subject<RestrictionalCircle> CircleExit { get; } = new Subject<RestrictionalCircle>();
        public Subject<RestrictionalCircle> CircleEnter { get; } = new Subject<RestrictionalCircle>();
        
        public float GetRadius() => transform.localScale.y / 2;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) 
                CircleExit.OnNext(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) 
                CircleEnter.OnNext(this);
        }
    }
}