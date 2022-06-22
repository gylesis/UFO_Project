namespace Project.Scripts.AI
{
    public struct HostileCreatureSpawnContext
    {
        public CreatureInfo Info;
        public IMovingPattern MovingPattern;
        public PolarVector Pos;
        public float Speed;
    }
}