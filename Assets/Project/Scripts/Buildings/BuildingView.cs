using UnityEngine;

namespace Project.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteBar _bar;

        public SpriteBar Bar => _bar;

        public void SetSpriteColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}