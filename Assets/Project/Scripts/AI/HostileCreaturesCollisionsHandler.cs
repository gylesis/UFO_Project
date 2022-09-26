using System;
using Project.PlayerLogic;
using UniRx;

namespace Project.AI
{
    public class HostileCreaturesCollisionsHandler : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly PlayerHealthService _playerHealthService;

        public HostileCreaturesCollisionsHandler(PlayerHealthService playerHealthService)
        {
            _playerHealthService = playerHealthService;
        }

        public void Handle(HostileCreature hostileCreature)
        {
            hostileCreature.Collision.Subscribe((OnCollision)).AddTo(_compositeDisposable);
        }

        private void OnCollision(HostileCreatureCollisionContext context)
        {
            if (context.CollisionObj.CompareTag("PlayerColliderForMisc"))
            {
                _playerHealthService.ApplyDamage(1);
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}