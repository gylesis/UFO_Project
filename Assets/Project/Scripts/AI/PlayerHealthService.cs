using System;
using Project.Scripts.Player;
using UniRx;

namespace Project.Scripts.AI
{
    public class PlayerHealthService
    {
        public Subject<int> HealthChanged { get; } = new Subject<int>();
        public Subject<Unit> Died { get; } = new Subject<Unit>();
        
        private int _health = 3;
        
        private readonly PlayerContainer _playerContainer;

        public PlayerHealthService(PlayerContainer playerContainer)
        {
            _playerContainer = playerContainer;
        }
        
        public void ApplyDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _health = 0;
                Died.OnNext(Unit.Default);
            }
            
            HealthChanged.OnNext(_health);
            _playerContainer.MakeInvulnerable(TimeSpan.FromSeconds(2));
        }
        
    }
}