using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.AI
{
    public class CloudsService : MonoBehaviour
    {
        [SerializeField] private int _cloudsCountToSpawn = 100;

        private readonly List<CalmCreature> _calmCreatures = new List<CalmCreature>();

        private CalmCreatureFactory _calmCreatureFactory;
        private MovableEntitiesWatcher _movableEntitiesWatcher;
        private CreaturesInfo _creaturesInfo;
        private CirclesRestrictionInfoService _circlesRestrictionInfoService;

        [Inject]
        private void Init(CalmCreatureFactory calmCreatureFactory,
            MovableEntitiesWatcher movableEntitiesWatcher, CreaturesInfo creaturesInfo,
            CirclesRestrictionInfoService circlesRestrictionInfoService)
        {
            _circlesRestrictionInfoService = circlesRestrictionInfoService;
            _creaturesInfo = creaturesInfo;
            _movableEntitiesWatcher = movableEntitiesWatcher;
            _calmCreatureFactory = calmCreatureFactory;

            Setup();
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            CreateClouds();
        }

        private void CreateClouds()
        {
            CreatureInfo creatureInfo = _creaturesInfo.GetCloudInfo();

            var secondLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(2);
            var firstLevelRadius = (int) _circlesRestrictionInfoService.GetRadius(1);

            var radiusOfLvlFirstAndTwo = secondLevelRadius - firstLevelRadius;

            for (int i = 1; i <= _cloudsCountToSpawn; i++)
            {
                var calmCreatureSpawnContext = new CalmCreatureSpawnContext();

                calmCreatureSpawnContext.CreatureInfo = creatureInfo;

                var angle = Random.Range(0, 360f);
                var speed = Random.Range(0.02f, 0.06f);
                int radius = Random.Range(firstLevelRadius, secondLevelRadius - ((10 / 100) * radiusOfLvlFirstAndTwo));
                var polarVector = new PolarVector(radius, angle);

                calmCreatureSpawnContext.Pos = polarVector;

                //IMovingPattern movingPattern = GetRandomPattern();
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

                calmCreatureSpawnContext.MovingPattern = movingPattern;
                calmCreatureSpawnContext.Speed = speed;

                CalmCreature calmCreature = _calmCreatureFactory.Create(calmCreatureSpawnContext);

                var needToBeOverPlayer = Random.value >= 0.7f;

                calmCreature.SpriteRenderer.sortingOrder = needToBeOverPlayer == true ? 2 : 0;

                _movableEntitiesWatcher.Watch(calmCreature);
                _calmCreatures.Add(calmCreature);
            }
        }

        public IMovingPattern GetRandomPattern()
        {
            var sinMoving = new SinMoving();
            var straightMoving = new StraightMoving();
            var backwardMoving = new BackwardMoving();

            List<IMovingPattern> patterns = new List<IMovingPattern>();

            patterns.Add(sinMoving);
            patterns.Add(straightMoving);
            patterns.Add(backwardMoving);

            var index = Random.Range(0, patterns.Count - 1);

            IMovingPattern movingPattern = patterns[index];
            return movingPattern;
        }
    }
}