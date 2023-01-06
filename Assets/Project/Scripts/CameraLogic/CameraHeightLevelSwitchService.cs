﻿using Project.PlayerLogic;

namespace Project.CameraLogic
{
    public class CameraHeightLevelSwitchService : IPlayerHeightLevelChangeListener
    {
        private readonly Config _config;
        private readonly CameraController _cameraController;

        public CameraHeightLevelSwitchService(CameraController cameraController, Config config)
        {
            _cameraController = cameraController;
            _config = config;
        }

        public void OnHeightLevelSwitch(SwitchHeightLevelContext switchHeightLevelContext)
        {
            var level = switchHeightLevelContext.Level;

            CameraHeightLevelInfo cameraHeightLevelInfo =
                _config.HeightLevelTransitionInfos[level - 1].CameraHeightLevelInfo;

            _cameraController.SwitchHeightLevel(cameraHeightLevelInfo);
        }
    }
}