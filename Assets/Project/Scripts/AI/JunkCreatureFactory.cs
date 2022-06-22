using Zenject;

namespace Project.Scripts.AI
{
    public class JunkCreatureFactory : IFactory<CreatureInfo,Creature>
    {
        private readonly DiContainer _container;

        public JunkCreatureFactory(DiContainer container)
        {
            _container = container;
        }
        
        public Creature Create(CreatureInfo info)
        {
            var creature = _container.InstantiatePrefabForComponent<Creature>(info.Prefab);

            return creature;
        }
    }
}