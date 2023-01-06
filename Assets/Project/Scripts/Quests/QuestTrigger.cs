using UniRx;
using UnityEditor;
using UnityEngine;

namespace Project.Quests
{
    [RequireComponent(typeof(Collider2D))]
    public class QuestTrigger : MonoBehaviour
    {
        [Tooltip("This Should be")] public Subject<Unit> Triggered { get; } = new Subject<Unit>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered.OnNext(Unit.Default);
            }
        }
    }

    [CustomEditor(typeof(QuestTrigger))]
    public class dfdsf : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "To work well, Quest Trigger should be attached in some condition composer (ReachDestinationComposer)",
                MessageType.Info);

            base.OnInspectorGUI();
        }
    }
}