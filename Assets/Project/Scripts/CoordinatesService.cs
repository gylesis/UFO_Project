using Project.Scripts.AI;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class CoordinatesService
    {
        private Transform _center;

        [Inject]
        private void Init(Planet planet)
        {
            _center = planet.Pivot;
        }

        public float GetRadius(Vector3 position)
        {
            Vector3 directionToTarget = GetRadiusVector(position);

            float distanceToTarget = directionToTarget.magnitude;

            return distanceToTarget;
        }

        public void SetRadius(Transform transform, float radius)
        {
            var angle = GetAngle(transform.position);

            var polarVector = new PolarVector(radius, angle);

            SetPolarCoords(transform, polarVector);
        }

        public void SetPolarCoords(Transform transform, PolarVector polarVector)
        {
            var angle = polarVector.Angle;

            angle *= Mathf.Deg2Rad;

            Vector3 position = transform.position;

            position.x = polarVector.Radius * Mathf.Cos(angle);
            position.y = polarVector.Radius * Mathf.Sin(angle);

            transform.position = position;
        }

        public Vector2 GetPolarCoords(PolarVector polarVector)
        {
            var radius = polarVector.Radius; // = distance to target

            var angle = polarVector.Angle;
            
            angle *= Mathf.Deg2Rad;

            Vector2 position;
            
            position.x = radius * Mathf.Cos(angle);
            position.y = radius * Mathf.Sin(angle);

            return position;
        }
        
        public Vector2 GetPolarCoords(Vector3 position, float angle)
        {
            var distanceToTarget = GetRadius(position);

            var radius = distanceToTarget; // = distance to target

            angle *= Mathf.Deg2Rad;

            position.x = radius * Mathf.Cos(angle);
            position.y = radius * Mathf.Sin(angle);

            return position;
        }

        public Vector2 GetPolarCoords(Vector3 position)
        {
            var distanceToTarget = GetRadius(position);

            var angle = GetAngle(position);

            var radius = distanceToTarget; // = distance to target

            position.x = radius * Mathf.Cos(angle);
            position.y = radius * Mathf.Sin(angle);

            return position;
        }

        private float GetAngle(Vector2 targetPos)
        {
            var directionToTarget = GetRadiusVector(targetPos);

            return Vector2.SignedAngle(_center.right, directionToTarget) * Mathf.Deg2Rad;
        }

        public float GetAngleDeg(Vector2 targetPos) => 
            GetAngle(targetPos) * Mathf.Rad2Deg;

        public Vector2 GetRadiusVector(Vector3 position) =>
            position - _center.position;
    }
}