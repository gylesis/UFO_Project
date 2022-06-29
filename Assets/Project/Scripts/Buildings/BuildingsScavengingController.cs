using System;
using Project.Scripts.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Buildings
{
    public class BuildingsScavengingController : IDisposable, ITickable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly PlayerContainer _playerContainer;
        private readonly BuildingScavengeConditionService _buildingScavengeConditionService;

        private Building _building;
        private IDisposable _scavengeStream;
        private IDisposable _timeDisposable;

        private bool _isPlayerScavenging;
        private BuildingScavengeService _buildingScavengeService;

        public BuildingsScavengingController(PlayerContainer playerContainer, BuildingScavengeConditionService buildingScavengeConditionService, BuildingScavengeService buildingScavengeService)
        {
            _buildingScavengeService = buildingScavengeService;
            _buildingScavengeConditionService = buildingScavengeConditionService;
            _playerContainer = playerContainer;
        }

        public void StartPollingToScavenge(Building building)
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
                        StartScavenging(building);
                    }
                }));
        }

        private async void StartScavenging(Building building)
        {
            var successClawRelease = await _playerContainer.Player.PlayerClawController.ReleaseClaw();

            if (successClawRelease == false) return;

            if (_buildingScavengeService.AbleToScavenge(building) == false) return;
            _buildingScavengeService.Scavenge(building);
            _isPlayerScavenging = true;
            
            _scavengeStream?.Dispose();
            _scavengeStream = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(((_) =>
            {
                if (_buildingScavengeService.AbleToScavenge(building) == false)
                {
                    StopScavenging();
                    return;
                }

                _buildingScavengeService.Scavenge(building);
                
            })).AddTo(_compositeDisposable);
        }

        public void StopScavenging()
        {
            Debug.Log("stop scavenge");
            _isPlayerScavenging = false;

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

            var isPlayerHasToStopScavenging = _buildingScavengeConditionService.Any();

            if (isPlayerHasToStopScavenging)
            {
                _building?.ScavengeTrigger.Disable();
                StopScavenging();
            }
        }
    }
}