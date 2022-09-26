using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerClawController
    {
        private readonly PlayerClaw _playerClaw;

        public Vector3 Position => _playerClaw.transform.position;

        public PlayerClawController(PlayerClaw playerClaw)
        {
            _playerClaw = playerClaw;
        }

        public async UniTask<bool> ReleaseClaw()
        {
            var releaseClaw = await _playerClaw.Release();

            return releaseClaw;
        }

        public async UniTask PullBack()
        {
            await _playerClaw.PullBack();
        }
    }
}