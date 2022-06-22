using UnityEngine;
using Zenject;

namespace Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Collider2D _collisionCollider;

        public Collider2D CollisionCollider => _collisionCollider;
        public Transform PlayerClaw => _playerClaw.transform;
        public PlayerView PlayerView => _playerView;
        public PlayerController PlayerController => _playerController;
        public PlayerClawController PlayerClawController => _playerClawController;
        public Transform Transform => _transform;
        
        private InputService _inputService;
        private PlayerClawController _playerClawController;
        private PlayerClaw _playerClaw;

        [Inject]
        private void Init(InputService inputService, PlayerClawController playerClawController, PlayerClaw playerClaw)
        {
            _playerClaw = playerClaw;
            _playerClawController = playerClawController;
            _inputService = inputService;
        }
    }
    
        
    
}