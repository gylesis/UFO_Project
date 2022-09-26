using Project.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Project.AI
{
    public class AntiAirDefense : MonoBehaviour
    {
        [SerializeField] private float _detectDistance = 40f;
        [Range(1, 90)] [SerializeField] private int _angleDetection = 70;
        [field: SerializeField] public Transform Krutilka { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        private StateMachine _stateMachine;
        private PlayerContainer _playerContainer;
        private AADStatesFactory _aadStatesFactory;

        [Inject]
        private void Init(PlayerContainer playerContainer, AADStatesFactory aadStatesFactory)
        {
            _aadStatesFactory = aadStatesFactory;
            _playerContainer = playerContainer;

            IState idleState = _aadStatesFactory.Create<IdleState>();
            IState attackState = _aadStatesFactory.Create<AttackState>();

            _stateMachine = new StateMachine(idleState, attackState);

            _stateMachine.AddAnyTransition(attackState, IsPlayerCloseFrustum);

            _stateMachine.AddTransition(attackState, idleState, (() => IsPlayerCloseDistance() == false));

            _stateMachine.ChangeState(idleState);
        }

        private bool IsPlayerCloseDistance()
        {
            Vector3 directionToPlayer = _playerContainer.Transform.position - transform.position;
            var distance = directionToPlayer.sqrMagnitude;
            var closeEnough = distance < _detectDistance * _detectDistance;

            return closeEnough;
        }

        private bool IsPlayerCloseFrustum()
        {
            Vector3 directionToPlayer = _playerContainer.Transform.position - transform.position;

            var angle = Vector3.Angle(directionToPlayer.normalized, transform.up);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);

            var distance = directionToPlayer.sqrMagnitude;
            var closeEnough = distance < _detectDistance * _detectDistance;

            float cosineRestriction = _angleDetection / 90f;

            if (cos >= cosineRestriction && closeEnough)
                return true;

            return false;
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}