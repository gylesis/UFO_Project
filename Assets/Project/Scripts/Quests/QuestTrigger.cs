using UniRx;
using UnityEngine;

namespace Project.Scripts.Quests
{
    [RequireComponent(typeof(Collider2D))]
    public class QuestTrigger : MonoBehaviour
    {
        public Subject<Unit> Triggered { get; } = new Subject<Unit>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered.OnNext(Unit.Default);
            }
        }

    }
}   