using UnityEngine;
using UnityEngine.UI;

namespace Warlords.Utils
{
    public class ImageSelectionView : UISelectionView
    {
        [SerializeField] private Image _outline;

        public override void Show()
        {
            _outline.enabled = true;
        }

        public override void Hide()
        {
            _outline.enabled = false;
        }
    }
}