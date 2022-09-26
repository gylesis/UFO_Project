using UnityEngine;

namespace Project.AI
{
    public class BackwardMoving : IMovingPattern
    {
        public float CurrentAngle { get; set; }
        public MovingEntityInfo Info { get; set; }

        public void Move(Transform transform, CoordinatesService coordinatesService)
        {
            CurrentAngle -= Info.Speed;

            if (CurrentAngle <= -360)
            {
                CurrentAngle = 0;
            }

            Vector2 polarCoords = coordinatesService.GetPolarCoords(transform.position, CurrentAngle);

            transform.position = polarCoords;
        }
    }

    public class MovingPatterBase
    {
        protected CoordinatesService CoordinatesService;

        public MovingPatterBase(CoordinatesService coordinatesService)
        {
            CoordinatesService = coordinatesService;
        }
    }
}