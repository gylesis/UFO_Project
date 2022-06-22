using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerClawController
    {
        private readonly PlayerClaw _playerClaw;

        public Vector3 Position => _playerClaw.transform.position;
        
        public PlayerClawController(PlayerClaw playerClaw)
        {
            _playerClaw = playerClaw;
        }

        public async UniTask<bool> ReleaseClaw(float height)
        {
            var releaseClaw = await _playerClaw.Release(height);

            return releaseClaw;
        }

        public async UniTask PullBack()
        {
            await _playerClaw.PullBack();
        }
    }
}