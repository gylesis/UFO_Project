namespace Project.Scripts.AI
{
    public class CreaturesFactory
    {
        private readonly JunkCreatureFactory _junkCreatureFactory;

        public CreaturesFactory(JunkCreatureFactory junkCreatureFactory)
        {
            _junkCreatureFactory = junkCreatureFactory;
        }

        public Creature CreateJunk(CreatureInfo creatureInfo)
        {
            Creature creature = _junkCreatureFactory.Create(creatureInfo);

            return creature;
        }
        
    }
}