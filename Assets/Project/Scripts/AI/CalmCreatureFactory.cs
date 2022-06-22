using Zenject;

namespace Project.Scripts.AI
{
    public class CalmCreatureFactory : PlaceholderFactory<CalmCreatureSpawnContext, CalmCreature> { }

    public struct CalmCreatureSpawnContext
    {
        public CreatureInfo CreatureInfo;
        public IMovingPattern MovingPattern;
        public PolarVector Pos;
        public float Speed;
    }

    public struct PolarVector
    {
        public float Radius;
        public float Angle;

        public PolarVector(float radius, float angle)
        {
            Angle = angle;
            Radius = radius;
        }
    }
}