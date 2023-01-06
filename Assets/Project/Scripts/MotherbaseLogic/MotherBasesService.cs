using Project.AI;
using UnityEngine;
using Zenject;

namespace Project.MotherbaseLogic
{
    public class MotherBasesService : MonoBehaviour
    {
        private MotherBaseFactory _motherBaseFactory;
        private CirclesRestrictionInfoService _circlesRestrictionInfoService;
        private MovableEntitiesWatcher _movableEntitiesWatcher;

        [Inject]
        private void Init(MotherBaseFactory motherBaseFactory,
            CirclesRestrictionInfoService circlesRestrictionInfoService, MovableEntitiesWatcher movableEntitiesWatcher)
        {
            _movableEntitiesWatcher = movableEntitiesWatcher;
            _circlesRestrictionInfoService = circlesRestrictionInfoService;
            _motherBaseFactory = motherBaseFactory;

            Setup();
        }

        public void Setup()
        {
            var motherBaseSpawnContext = new MotherBaseSpawnContext();

            float angle = 90;
            float radius = Constants.MotherBaseRadius;
            var polarVector = new PolarVector(radius, angle);

            motherBaseSpawnContext.Pos = polarVector;

            IMovingPattern movingPattern = new StraightMoving();

            var movingPatternInfo = new MovingEntityInfo();

            movingPatternInfo.Speed = 0.02f;
            movingPatternInfo.PolarVector = polarVector;

            movingPattern.CurrentAngle = angle;
            movingPattern.Info = movingPatternInfo;

            motherBaseSpawnContext.MovingPattern = movingPattern;

            var motherBase = _motherBaseFactory.Create(motherBaseSpawnContext);

            _movableEntitiesWatcher.Watch(motherBase);
        }
    }
}