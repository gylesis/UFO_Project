using DG.Tweening;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerReactionOnHeightLevelChangeService : IPlayerHeightLevelChangeListener
    {
        private readonly PlayerContainer _playerContainer;
        private readonly Config _config;

        public PlayerReactionOnHeightLevelChangeService(PlayerContainer playerContainer, Config config)
        {
            _config = config;
            _playerContainer = playerContainer;
        }

        public void OnHeightLevelSwitch(SwitchHeightLevelContext switchHeightLevelContext)
        {
            ScalePlayerView(switchHeightLevelContext);

            int level = switchHeightLevelContext.Level;

            PlayerHeightLevelInfo heightLevelInfo = _config.HeightLevelTransitionInfos[level - 1].PlayerHeightLevelInfo;
            
            float speed = heightLevelInfo.Speed;
    
            _playerContainer.Player.PlayerController.SetXSpeed(speed);
            _playerContainer.Player.PlayerController.SetYSpeed(speed);
        }

        private void ScalePlayerView(SwitchHeightLevelContext switchHeightLevelContext)
        {
            var level = switchHeightLevelContext.Level;

            Vector3 scale = Vector3.one;

            if (level == 3)
            {
                scale *= 2.5f;
            }
            else if (level == 2)
            {
                scale *= 1.4f;
            }
            else if (level == 1)
            {
                scale *= 1f;
            }

            CameraHeightLevelInfo cameraHeightLevelInfo = _config.HeightLevelTransitionInfos[level - 1].CameraHeightLevelInfo;
            
            _playerContainer.Player.PlayerView.transform.DOScale(scale, 0.5f).SetEase(cameraHeightLevelInfo.PosTransition);
        }
    }
}