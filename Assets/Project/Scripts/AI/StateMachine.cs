namespace Project.Scripts.AI
{
    public class StateMachine { }

    public interface IState
    {
        void Enter();
        void Exit();
    }

    public class CalmState : IState
    {
        public void Enter() { }

        public void Exit() { }
    }
}