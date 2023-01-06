using System.Linq;
using Project.AI;
using Project.Buildings;
using Project.CameraLogic;
using Project.Infrastructure;
using Project.MotherbaseLogic;
using Project.PlayerLogic;
using Project.Quests;
using Project.Quests.QuestsConditions;
using UnityEngine;
using Zenject;

namespace Project
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private Player _player;
        [SerializeField] private Planet _planet;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private CreaturesInfo _creaturesInfo;

        [SerializeField] private GameObject _hostileCreaturePrefab;
        [SerializeField] private GameObject _calmCreaturePrefab;

        [SerializeField] private GameObject _motherBasePrefab;

        [SerializeField] private HostileCreaturesService _hostileCreaturesService; //

        [SerializeField] private RestrictionalCircle[] _restrictionalCircles;

        [SerializeField] private QuestView _questView;
        [SerializeField] private QuestsContainer _questsContainer;
        [SerializeField] private ReachDestinationGoalsConditionComposer _reachDestinationGoalsConditionComposer;

        [SerializeField] private Transform _calmCreaturesParent;
        [SerializeField] private Transform _hostileCreaturesParent;
        [SerializeField] private AntiAirDefenseMissile _missile;

        public override void InstallBindings()
        {
            BindQuests();

            Container.BindInterfacesAndSelfTo<BuildingsInitializeService>().AsSingle();

            Container.Bind<AntiAirDefenseMissile>().FromInstance(_missile).AsSingle();

            var antiAirDefense = FindObjectOfType<AntiAirDefense>();
            Container.Bind<AntiAirDefense>().FromInstance(antiAirDefense).AsSingle();

            //Container.BindFactory<int, IState, AntiAirDefenseFactory>().FromFactory<AntiAirDefenseFactory2>();

            Container.BindInterfacesAndSelfTo<BuildingResourcesController>().AsSingle().NonLazy();

            Container.Bind<AADStatesFactory>().AsSingle();
            
            Container.Bind<SaveDataLoadAndSaveService>().AsSingle().NonLazy();

            
            Container.Bind<BuildingScavengeService>().AsSingle();
           

            Container.BindFactory<MotherBaseSpawnContext, MotherBase, MotherBaseFactory>()
                .FromComponentInNewPrefab(_motherBasePrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<HostileCreaturesCollisionsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthService>().AsSingle();

            Container.Bind<CreaturesInfo>().FromInstance(_creaturesInfo).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerHeightCheckService>().AsSingle().NonLazy();
            Container.Bind<PlayerHeightLevelDispatcher>().AsSingle().NonLazy();
            Container.Bind<RestrictionalCircle[]>().FromInstance(_restrictionalCircles).AsSingle();
            Container.BindInterfacesAndSelfTo<CirclesRestrictionInfoService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerReactionOnHeightLevelChangeService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<CameraHeightLevelSwitchService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<HostileCreaturesService>().FromInstance(_hostileCreaturesService)
                .AsSingle();
            Container.Bind<Planet>().FromInstance(_planet).AsSingle();
            Container.Bind<CoordinatesService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
            Container.Bind<CameraContainer>().FromInstance(_cameraContainer).AsSingle();

            var buildings = FindObjectsOfType<Building>();

            Container.Bind<Building[]>().FromInstance(buildings).AsSingle();

            var scavengeTriggers = buildings.Select(x => x.ScavengeTrigger).ToArray();

            Container.BindInterfacesAndSelfTo<BuildingsScavengingHandler>().AsSingle().WithArguments(scavengeTriggers)
                .NonLazy();

            Container.Bind<PlayerWallet>().AsSingle().NonLazy();
            Container.Bind<PlayerContainer>().AsSingle().NonLazy();
            Container.Bind<BuildingScavengeConditionService>().AsSingle().NonLazy();
            Container.Bind<HeightRestrictService>().AsSingle();

            Container.BindInterfacesAndSelfTo<MovableEntitiesWatcher>().AsSingle().NonLazy();

            Container.BindFactory<CalmCreatureSpawnContext, CalmCreature, CalmCreatureFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CalmCreatureInstaller>(_calmCreaturePrefab)
                .UnderTransform(_calmCreaturesParent)
                .AsSingle();

            Container.BindFactory<HostileCreatureSpawnContext, HostileCreature, HostileCreaturesFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<HostileCreatureInstaller>(_hostileCreaturePrefab)
                .UnderTransform(_hostileCreaturesParent)
                .AsSingle();

            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_player)
                .UnderTransform(_playerSpawnPoint)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BuildingsScavengingController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BuildingsRegistrationService>().AsSingle();
        }

        private void BindQuests()
        {
            Container.BindInterfacesAndSelfTo<QuestsController>().AsSingle();

            Container.Bind<QuestView>().FromInstance(_questView).AsSingle();
            Container.Bind<QuestsContainer>().FromInstance(_questsContainer).AsSingle();

            Container.Bind<QuestGoalsConditionResolver>().AsSingle();

            Container.BindInterfacesAndSelfTo<ReachDestinationGoalsConditionComposer>()
                .FromInstance(_reachDestinationGoalsConditionComposer).AsSingle();
            Container.BindInterfacesAndSelfTo<ScavengeResourcesConditionComposer>().AsSingle();
        }
    }

    public class MotherBaseInstaller : Installer
    {
        public override void InstallBindings() { }
    }
}