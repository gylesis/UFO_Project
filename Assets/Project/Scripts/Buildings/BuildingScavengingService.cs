using System;
using Project.Scripts.Camera;
using Project.Scripts.Player;
using UniRx;
using UnityEngine;
using Zenject;
using static UnityEngine.Screen;

namespace Project.Scripts.Buildings
{
    public class BuildingScavengingService : IDisposable, ITickable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly PlayerContainer _playerContainer;
        private readonly PlayerWallet _playerWallet;
        private readonly PlayerCameraCondition _playerCameraCondition;

        private Building _building;
        private IDisposable _scavengeStream;
        private IDisposable _timeDisposable;

        private bool _isPlayerScavenging;

        public BuildingScavengingService(PlayerContainer playerContainer, PlayerWallet playerWallet,
            PlayerCameraCondition playerCameraCondition)
        {
            _playerCameraCondition = playerCameraCondition;
            _playerWallet = playerWallet;
            _playerContainer = playerContainer;
        }

        public void StartScavenging(Building building) // govnocode
        {
            float time = 0;

            _timeDisposable?.Dispose();
            _timeDisposable = Observable
                .EveryUpdate()
                .Subscribe((_ =>
                {
                    time += Time.deltaTime;

                    if (time > 1)
                    {
                        _timeDisposable.Dispose();
                        Scavenge(building);
                    }
                }));
        }

        private async void Scavenge(Building building)
        {
            Debug.Log("scavenge");
            var successRelease = await _playerContainer.Player.PlayerClawController.ReleaseClaw(1); 

            if(successRelease == false) return;
            
            _scavengeStream?.Dispose();
            _scavengeStream = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(((_) =>
            {
                ScavengeBuilding(building);
            })).AddTo(_compositeDisposable);

            _isPlayerScavenging = true;
        }

        private void ScavengeBuilding(Building building)
        {
            _building = building;
            _playerWallet.AddTenge((int) building.Data.Height * 10);
        }

        public void StopScavenging(Building building)
        {
            //if(_isPlayerScavenging == false) return;
            
            Debug.Log("stop scavenge");
            _isPlayerScavenging = false;
            //_playerContainer.Player.PlayerView.StopShake();

            _playerContainer.Player.PlayerClawController.PullBack();
            _timeDisposable?.Dispose();
            _scavengeStream?.Dispose();
            //_playerContainer.StopLerp();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _scavengeStream?.Dispose();
        }

        public void Tick()
        {
            if (_isPlayerScavenging == false) return;

            var isPlayerHasToStopScavenging = _playerCameraCondition.IsPlayerHasToStopScavenging();

            if (isPlayerHasToStopScavenging)
            {
                _building?.ScavengeTrigger.Disable();
                StopScavenging(null);
            }
        }
    }

    public class PlayerCameraCondition
    {
        private readonly CameraContainer _cameraContainer;
        private Transform _playerClaw;
        private PlayerContainer _playerContainer;

        public PlayerCameraCondition(PlayerContainer playerContainer, CameraContainer cameraContainer)
        {
            _playerContainer = playerContainer;
            _cameraContainer = cameraContainer;
            _playerClaw = playerContainer.Player.PlayerClaw.transform;
        }

        public bool IsPlayerHasToStopScavenging()
        {
            var distance = (_playerClaw.transform.position - _playerContainer.Player.transform.position).sqrMagnitude;

            return distance > 40;
        }
    }
}