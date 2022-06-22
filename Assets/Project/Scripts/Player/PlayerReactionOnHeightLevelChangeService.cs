using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerReactionOnHeightLevelChangeService : IPlayerHeightLevelChangeListener
    {
        private readonly PlayerContainer _playerContainer;
        private Config _config;

        public PlayerReactionOnHeightLevelChangeService(PlayerContainer playerContainer, Config config)
        {
            _config = config;
            _playerContainer = playerContainer;
        }
        
        public void OnHeightLevelSwitch(SwitchHeightLevelContext switchHeightLevelContext)
        {
            ScalePlayerView(switchHeightLevelContext);

            int level = switchHeightLevelContext.Level;

            float size = _config._heightLevelTransitionInfo[level - 1].PlayerHeightLevelInfo.Size;
            
            _playerContainer.Player.PlayerController.SetXSpeed(size);
            _playerContainer.Player.PlayerController.SetYSpeed(size);
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

            _playerContainer.Player.PlayerView.transform.DOScale(scale, 0.5f)
                .SetEase(_config._heightLevelTransitionInfo[level - 1].CameraHeightLevelInfo.PosTransition);
        }
    }
}