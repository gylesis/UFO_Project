using Project.Scripts.AI;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class CalmCreatureInstaller : MonoInstaller
    {
        private CalmCreatureSpawnContext _calmCreatureSpawnContext;

        [Inject]
        private void Init(CalmCreatureSpawnContext calmCreatureSpawnContext)
        {
            _calmCreatureSpawnContext = calmCreatureSpawnContext;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<CalmCreatureSpawnContext>().FromInstance(_calmCreatureSpawnContext).AsSingle();
            Container.Bind<CreaturesPlacer>().AsSingle().WithArguments(transform, _calmCreatureSpawnContext.Pos).NonLazy();
            Container.Bind<CalmCreature>().FromComponentOnRoot().AsSingle();
        }
    }
}