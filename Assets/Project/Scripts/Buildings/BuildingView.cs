using UnityEngine;

namespace Project.Scripts.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSpriteColor(Color color)
        {
            _spriteRenderer.color = color;
        }
        
        
    }
}