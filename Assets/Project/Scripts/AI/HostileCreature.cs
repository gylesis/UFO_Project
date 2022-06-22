using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.AI
{
    public class HostileCreature : MonoBehaviour, IMovableEntity, IHostile
    {
        private CoordinatesService _coordinatesService;
        private IMovingPattern _movingPattern;
        public IMovingPattern MovingPattern => _movingPattern;

        public Subject<HostileCreatureCollisionContext> Collision { get; } =
            new Subject<HostileCreatureCollisionContext>();

        [Inject]
        private void Init(CoordinatesService coordinatesService, HostileCreatureSpawnContext context)
        {
            _coordinatesService = coordinatesService;

            _movingPattern = context.MovingPattern;
            _movingPattern.CurrentAngle = context.Pos.Angle;
        }

        public void ChangeMovingPattern(IMovingPattern movingPattern)
        {
            _movingPattern = movingPattern;
            
            var movingPatternInfo = new MovingEntityInfo();
            movingPatternInfo.PolarVector = _movingPattern.Info.PolarVector;

            _movingPattern.Info = movingPatternInfo;
        }

        public void MoveTick()
        {
            _movingPattern.Move(transform, _coordinatesService);

            transform.up = _coordinatesService.GetRadiusVector(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hostileCreatureCollisionContext = new HostileCreatureCollisionContext();

            hostileCreatureCollisionContext.Sender = this;
            hostileCreatureCollisionContext.CollisionObj = other;

            Collision.OnNext(hostileCreatureCollisionContext);
        }
    }
}