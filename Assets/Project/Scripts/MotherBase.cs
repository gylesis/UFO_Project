using Project.Scripts.AI;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MotherBase : MonoBehaviour, IMovableEntity
    {
        private IMovingPattern _movingPattern;
        private CoordinatesService _coordinatesService;
        public IMovingPattern MovingPattern => _movingPattern;

        [Inject]
        private void Init(CoordinatesService coordinatesService, MotherBaseSpawnContext context)
        {
            _coordinatesService = coordinatesService;

            _movingPattern = context.MovingPattern;
        }

        public void ChangeMovingPattern(IMovingPattern movingPattern)
        {
            /*_movingPattern = movingPattern;

            var movingPatternInfo = new MovingEntityInfo();
            movingPatternInfo.PolarVector = _movingPattern.Info.PolarVector;

            _movingPattern.Info = movingPatternInfo;*/
        }

        public void MoveTick()
        {
            _movingPattern.Move(transform,_coordinatesService);

            transform.up = _coordinatesService.GetRadiusVector(transform.position);
        }
    }

    public struct MotherBaseSpawnContext
    {
        public PolarVector Pos;
        public IMovingPattern MovingPattern;
    }
}