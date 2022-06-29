using System;
using Project.Scripts.Player;
using UniRx;
using Zenject;

namespace Project.Scripts.AI
{
    public class PlayerHealthService : IInitializable
    {
        public Subject<int> HealthChanged { get; } = new Subject<int>();
        public Subject<Unit> Died { get; } = new Subject<Unit>();
        
        private int _health = 3;
        
        private readonly PlayerContainer _playerContainer;
        private Config _config;

        public PlayerHealthService(PlayerContainer playerContainer, Config config)
        {
            _config = config;
            _playerContainer = playerContainer;
        }

        public void Initialize()
        {
            _health = _config.PlayerStats.HealthCount;

            HealthChanged.OnNext(_health);
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