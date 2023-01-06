using UnityEngine;

namespace Project.PlayerLogic
{
    public class UpgradeUIViewData : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Transform Parent => _parent;
        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}