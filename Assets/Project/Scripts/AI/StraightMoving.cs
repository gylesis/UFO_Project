using UnityEngine;

namespace Project.Scripts.AI
{
    public class StraightMoving :  IMovingPattern
    {
        public float CurrentAngle { get; set; }
        public MovingEntityInfo Info { get; set; }

        public void Move(Transform transform, CoordinatesService coordinatesService)
        {
            CurrentAngle += Info.Speed;

            if (CurrentAngle >= 360)
            {
                CurrentAngle = 0;
            }

            Vector2 polarCoords = coordinatesService.GetPolarCoords(transform.position, CurrentAngle);

            transform.position = polarCoords;
        }
    }
}