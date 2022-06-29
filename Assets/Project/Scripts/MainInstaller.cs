using System.Linq;
using Project.Scripts.AI;
using Project.Scripts.Buildings;
using Project.Scripts.Camera;
using Project.Scripts.Player;
using Project.Scripts.Quests;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private Config _config;
        [SerializeField] private Player.Player _player;
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
        
        public override void InstallBindings()
        {
            BindQuests();

            
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
            
            Container.BindInterfacesAndSelfTo<HostileCreaturesService>().FromInstance(_hostileCreaturesService).AsSingle();
            Container.Bind<Planet>().FromInstance(_planet).AsSingle();
            Container.Bind<CoordinatesService>().AsSingle().NonLazy();
            Container.Bind<Config>().FromInstance(_config).AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
            Container.Bind<CameraContainer>().FromInstance(_cameraContainer).AsSingle();

            var scavengeTriggers = FindObjectsOfType<Building>().Select(x => x.ScavengeTrigger).ToArray();

            Container.BindInterfacesAndSelfTo<BuildingsScavengingHandler>().AsSingle().WithArguments(scavengeTriggers).NonLazy();

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
            
            Container.Bind<Player.Player>()
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

            Container.BindInterfacesAndSelfTo<ReachDestinationGoalsConditionComposer>().FromInstance(_reachDestinationGoalsConditionComposer).AsSingle();
            Container.BindInterfacesAndSelfTo<ScavengeResourcesConditionComposer>().AsSingle();
        }
        
    }

    public class MotherBaseInstaller : Installer
    {
        public override void InstallBindings()
        {
            
        }
    }

    
}