using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.AI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;  
        
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private PlayerHealthService _playerHealthService;

        [Inject]
        private void Init(PlayerHealthService playerHealthService)
        {
            _playerHealthService = playerHealthService;
        }

        private void Start()
        {
            _playerHealthService.HealthChanged.Subscribe((OnHealthChanged)).AddTo(_compositeDisposable);
        }

        private async void OnHealthChanged(int health)
        {
            Color initColor = _healthText.color;

            await _healthText.DOColor(Color.red, 0.5f).AsyncWaitForCompletion();

            _healthText.text = health.ToString();
            
            _healthText.DOColor(initColor, 0.5f);
        }

        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }
    }
}