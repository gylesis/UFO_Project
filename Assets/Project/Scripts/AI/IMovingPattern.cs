using UnityEngine;

namespace Project.Scripts.AI
{
    public interface IMovingPattern
    {
        float CurrentAngle { get; set; }
        MovingEntityInfo Info { get; set; }
        void Move(Transform transform, CoordinatesService coordinatesService);
    }

    public struct MovingEntityInfo
    {
        public PolarVector PolarVector;
        public float Speed;
    }
}