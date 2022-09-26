using UnityEngine;

namespace Project.AI
{
    public class SinMoving : IMovingPattern
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

            var sin = Mathf.Sin(CurrentAngle);

            coordinatesService.SetRadius(transform, Info.PolarVector.Radius + sin);

            Vector2 polarCoords = coordinatesService.GetPolarCoords(transform.position, CurrentAngle);

            transform.position = polarCoords;

            // sin = Mathf.PingPong(sin, 1);
        }
    }
}