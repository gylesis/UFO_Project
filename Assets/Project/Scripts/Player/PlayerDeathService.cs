using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Scripts.AI
{
    public class PlayerDeathService : MonoBehaviour
    {
        [SerializeField] private TMP_Text _deathSign;
        
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private PlayerContainer _playerContainer;

        [Inject]
        public void Init(PlayerHealthService playerHealthService, PlayerContainer playerContainer)
        {
            _playerContainer = playerContainer;
            playerHealthService.Died.Subscribe((OnPlayerDead)).AddTo(_compositeDisposable);
        }

        private async void OnPlayerDead(Unit _)
        {
            _playerContainer.Player.PlayerController.IsStickingLocked = true;
            _playerContainer.MakeInvulnerable();
            
            _deathSign.enabled = true;
            _deathSign.text = "Ты сдох чел";

            await UniTask.Delay(TimeSpan.FromSeconds(5));

            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }
    }
}