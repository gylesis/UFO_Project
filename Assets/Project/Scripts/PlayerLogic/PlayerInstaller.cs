using UnityEngine;
using Zenject;

namespace Project.Scripts.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerClaw _playerClaw;    
        public override void InstallBindings()
        {
            Container.Bind<PlayerClawController>().AsSingle();
            Container.Bind<Player>().FromComponentOnRoot().AsSingle();
            Container.Bind<PlayerClaw>().FromInstance(_playerClaw).AsSingle();
        }
    }
}