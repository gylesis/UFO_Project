using Zenject;

namespace Project.AI
{
    public class HostileCreatureInstaller : MonoInstaller
    {
        private HostileCreatureSpawnContext _hostileCreatureSpawnContext;

        [Inject]
        private void Init(HostileCreatureSpawnContext hostileCreatureSpawnContext)
        {
            _hostileCreatureSpawnContext = hostileCreatureSpawnContext;
        }

        public override void InstallBindings()
        {
            Container.Bind<HostileCreature>().FromComponentOnRoot().AsSingle();
            Container.Bind<HostileCreatureSpawnContext>().FromInstance(_hostileCreatureSpawnContext).AsSingle();
            Container.Bind<CreaturesPlacer>().AsSingle().WithArguments(transform, _hostileCreatureSpawnContext.Pos)
                .NonLazy();
        }
    }
}