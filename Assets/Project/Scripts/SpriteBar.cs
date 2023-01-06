using UnityEngine;

namespace Project
{
    public class SpriteBar : MonoBehaviour
    {
        [SerializeField] private Transform _barParent;

        public void UpdateValue(float value)
        {
            Vector3 localScale = _barParent.transform.localScale;

            localScale.x = value;
            _barParent.transform.localScale = localScale;
        }
    }
}