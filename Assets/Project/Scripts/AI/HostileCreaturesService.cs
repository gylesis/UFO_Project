using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.AI
{
    public class HostileCreaturesService : MonoBehaviour, IInitializable
    {
        [SerializeField] private int _junkCountToSpawn = 50;
        [SerializeField] private int _boltsCountToSpawn = 50;

        private CirclesRestrictionInfoService _circlesRestrictionInfoService;
        private CreaturesInfo _creaturesInfo;
        private MovableEntitiesWatcher _movableEntitiesWatcher;
        private HostileCreaturesFactory _hostileCreaturesFactory;

        private List<HostileCreature> _hostileCreatures = new List<HostileCreature>();
        private HostileCreaturesCollisionsHandler _hostileCreaturesCollisionsHandler;

        [Inject]
        private void Init(HostileCreaturesFactory hostileCreaturesFactory,
            MovableEntitiesWatcher movableEntitiesWatcher, CreaturesInfo creaturesInfo,
            CirclesRestrictionInfoService circlesRestrictionInfoService,
            HostileCreaturesCollisionsHandler hostileCreaturesCollisionsHandler)
        {
            _hostileCreaturesCollisionsHandler = hostileCreaturesCollisionsHandler;
            _hostileCreaturesFactory = hostileCreaturesFactory;
            _circlesRestrictionInfoService = circlesRestrictionInfoService;
            _creaturesInfo = creaturesInfo;
            _movableEntitiesWatcher = movableEntitiesWatcher;
        }

        public void Initialize()
        {
            CreateJunk();   

            // CreateBolts();
        }

        private void CreateJunk()
        {
            CreatureInfo creatureInfo = _creaturesInfo.GetJunkInfo();

            var secondLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(2);
            var thirdLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(3);

            var radiusOfLvlTwoAndThree = thirdLevelRadius - secondLevelRadius;

            for (int i = 1; i <= _junkCountToSpawn; i++)
            {
                var angle = Random.Range(0, 360f);
                var speed = Random.Range(0.02f, 0.03f);
                int radius = Random.Range(secondLevelRadius, thirdLevelRadius - radiusOfLvlTwoAndThree / 2);
                var polarVector = new PolarVector(radius, angle);

                IMovingPattern movingPattern;

                if (Random.value > 0.5f)
                {
                    movingPattern = new StraightMoving();
                }
                else
                {
                    movingPattern = new BackwardMoving();
                }

                var movingPatternInfo = new MovingEntityInfo();

                movingPatternInfo.PolarVector = polarVector;
                movingPatternInfo.Speed = speed;
                
                movingPattern.Info = movingPatternInfo;
                movingPattern.CurrentAngle = angle;

                var hostileCreatureSpawnContext = new HostileCreatureSpawnContext();

                hostileCreatureSpawnContext.Info = creatureInfo;
                hostileCreatureSpawnContext.Pos = polarVector;
                hostileCreatureSpawnContext.MovingPattern = movingPattern;
                hostileCreatureSpawnContext.Speed = speed;

                HostileCreature hostileCreature = _hostileCreaturesFactory.Create(hostileCreatureSpawnContext);

                _movableEntitiesWatcher.Watch(hostileCreature);
                _hostileCreatures.Add(hostileCreature);
                _hostileCreaturesCollisionsHandler.Handle(hostileCreature);
            }
        }

        private void CreateBolts()
        {
            CreatureInfo creatureInfo = _creaturesInfo.GetBoltInfo();

            var secondLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(2);
            var thirdLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(3);

            var radiusOfLvlTwoAndThree = thirdLevelRadius - secondLevelRadius;

            for (int i = 1; i <= _boltsCountToSpawn; i++)
            {
                var hostileCreatureSpawnContext = new HostileCreatureSpawnContext();

                hostileCreatureSpawnContext.Info = creatureInfo;
                
                var angle = Random.Range(0, 360f);
                var speed = Random.Range(0.02f, 0.03f);
                int radius = Random.Range(secondLevelRadius, thirdLevelRadius - radiusOfLvlTwoAndThree / 2);
                var polarVector = new PolarVector(radius, angle);

                hostileCreatureSpawnContext.Pos = polarVector;

                IMovingPattern movingPattern = new StraightMoving();

                var movingPatternInfo = new MovingEntityInfo();
                movingPatternInfo.PolarVector = polarVector;
                movingPatternInfo.Speed = speed;

                movingPattern.Info = movingPatternInfo;

                hostileCreatureSpawnContext.MovingPattern = movingPattern;
                hostileCreatureSpawnContext.Speed = speed;

                HostileCreature hostileCreature = _hostileCreaturesFactory.Create(hostileCreatureSpawnContext);

                _movableEntitiesWatcher.Watch(hostileCreature);
                _hostileCreatures.Add(hostileCreature);
                _hostileCreaturesCollisionsHandler.Handle(hostileCreature);
            }
        }
    }

    public struct HostileCreatureCollisionContext
    {
        public HostileCreature Sender;
        public Collider2D CollisionObj;
    }
}