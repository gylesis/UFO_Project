using System;
using UniRx;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerHeightCheckService : IDisposable
    {
        private PlayerHeightLevelDispatcher _playerHeightLevelDispatcher;
        private PlayerContainer _playerContainer;
        private CirclesRestrictionInfoService _circlesRestrictionInfoService;
        private CoordinatesService _coordinatesService;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private int _currentLevel = 1;

        public PlayerHeightCheckService(PlayerContainer playerContainer,
            PlayerHeightLevelDispatcher playerHeightLevelDispatcher,
            CirclesRestrictionInfoService circlesRestrictionInfoService, CoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
            _circlesRestrictionInfoService = circlesRestrictionInfoService;
            _playerContainer = playerContainer;
            _playerHeightLevelDispatcher = playerHeightLevelDispatcher;

            SetHeightLevel(1);

            _circlesRestrictionInfoService.CircleExit.Subscribe((OnCircleExit)).AddTo(_compositeDisposable);
            _circlesRestrictionInfoService.CircleEnter.Subscribe(((OnCircleEnter))).AddTo(_compositeDisposable);
        }

        private void OnCircleEnter(RestrictionalCircle circle)
        {
            GoPrevHeightLevel();
        }

        private void OnCircleExit(RestrictionalCircle circle)
        {
            GoNextHeightLevel();
        }

        private void SetHeightLevel(int level)
        {
            _currentLevel = level;

            Invoke();
        }

        private void GoPrevHeightLevel()
        {
            _currentLevel--;

            Invoke();
        }

        private void GoNextHeightLevel()
        {
            _currentLevel++;

            Invoke();
        }

        private void Invoke()
        {
            _currentLevel = Mathf.Clamp(_currentLevel, 1, 3);

            var switchHeightLevelContext = new SwitchHeightLevelContext();
            switchHeightLevelContext.Level = _currentLevel;

            _playerHeightLevelDispatcher.OnSwitchHeightLevel(switchHeightLevelContext);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}