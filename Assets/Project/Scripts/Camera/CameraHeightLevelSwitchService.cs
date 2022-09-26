using Project.PlayerLogic;

namespace Project.Camera
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
                _config._heightLevelTransitionInfo[level - 1].CameraHeightLevelInfo;

            _cameraController.SwitchHeightLevel(cameraHeightLevelInfo);
        }
    }
}