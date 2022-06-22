using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _shakeTransform;

        [SerializeField] private int _vibrato = 45;
        [SerializeField] private float _randomness = 5;
        [SerializeField] private float _strength;

        [SerializeField] private SpriteRenderer _ufoLightSprite;

        [SerializeField] private SpriteRenderer _ufoSprite;
        
        private Tweener _shakeTween;

        private bool _allowToShake;

        private Color _defaultColor;
        
        public void ColorUFO(Color color)
        {
            _defaultColor = _ufoSprite.color;
            _ufoSprite.color = color;
        }

        public void ColorDefault()
        {
            _ufoSprite.color = _defaultColor;
        }
        
        private void Awake()
        {
            _ufoLightSprite.enabled = false;
        }

        public void Shake()
        {
            _ufoLightSprite.enabled = true;
            _allowToShake = true;
            _shakeTween?.Complete();
            
            _shakeTween = _shakeTransform.DOShakeRotation(3, transform.forward * 10, _vibrato, _randomness).OnComplete((
                () =>
                {
                    if(_allowToShake == false) return;
                    
                    Shake();
                }));
        }

        public void StopShake()
        {
            _ufoLightSprite.enabled = false;
            _allowToShake = false;
            _shakeTween?.Kill(true);
            _shakeTween?.Complete();
        }
    }
}