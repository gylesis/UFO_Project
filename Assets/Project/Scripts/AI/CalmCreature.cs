using UnityEngine;
using Zenject;

namespace Project.AI
{
    public class CalmCreature : MonoBehaviour, IMovableEntity
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private CoordinatesService _coordinatesService;

        private IMovingPattern _movingPattern;
        public IMovingPattern MovingPattern => _movingPattern;

        [Inject]
        private void Init(CoordinatesService coordinatesService, CalmCreatureSpawnContext context)
        {
            _coordinatesService = coordinatesService;

            _movingPattern = context.MovingPattern;
            _movingPattern.CurrentAngle = context.Pos.Angle;
        }

        public void ChangeMovingPattern(IMovingPattern pattern)
        {
            _movingPattern = pattern;

            var movingPatternInfo = new MovingEntityInfo();
            movingPatternInfo.PolarVector = _movingPattern.Info.PolarVector;
            movingPatternInfo.Speed = pattern.Info.Speed;

            _movingPattern.Info = movingPatternInfo;
        }

        public void MoveTick()
        {
            _movingPattern.Move(transform, _coordinatesService);
            transform.up = _coordinatesService.GetRadiusVector(transform.position);
        }
    }
}