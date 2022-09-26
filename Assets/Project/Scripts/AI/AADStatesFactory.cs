using Zenject;

namespace Project.AI
{
    public class AADStatesFactory
    {
        private readonly DiContainer _container;

        public AADStatesFactory(DiContainer container)
        {
            _container = container;
        }

        public TStateType Create<TStateType>() where TStateType : IState
        {
            TStateType state = _container.Instantiate<TStateType>();

            // Debug.Log($"Created state with {typeof(TStateType)}");

            return state;
        }
    }
}