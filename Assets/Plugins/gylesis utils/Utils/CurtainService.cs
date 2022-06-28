using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Warlords.Utils
{
    public class CurtainService : MonoBehaviour
    {
        [SerializeField] private GameObject _curtainObj;
        [SerializeField] private Image _progressImage;
     //   [SerializeField] private Image _blockImage; 
     
        private IDisposable _sceneLoadProgressDisposable;
        private Tweener _progressTweener;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Show()
        {
            _progressImage.fillAmount = 1;
            _progressImage.enabled = true;
            _curtainObj.SetActive(true);
        }

        public void ShowWithProgress(FloatReactiveProperty progressProperty)
        {
            _sceneLoadProgressDisposable?.Dispose();
            
            _progressImage.fillAmount = 0;
            _progressImage.enabled = true;
            _curtainObj.SetActive(true);

            _sceneLoadProgressDisposable = progressProperty.Subscribe(UpdateView);
        }

        private void UpdateView(float progressValue)
        {
            _progressTweener?.Complete();
            
            var startProgressValue = _progressImage.fillAmount;

            _progressTweener = DOVirtual.Float(startProgressValue,progressValue, 0.5f, value => _progressImage.fillAmount = value);
        }
        
        public void Hide()
        {
            _progressImage.enabled = false;
            _curtainObj.SetActive(false);
        }

        private void OnDestroy()
        {
            _sceneLoadProgressDisposable?.Dispose();
        }
    }
}