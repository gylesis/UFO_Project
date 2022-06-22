using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Project.Scripts
{
    public class CirclesRestrictionInfoService : IDisposable
    {
        private readonly RestrictionalCircle[] _restrictionalCircles;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public Subject<RestrictionalCircle> CircleExit { get; } = new Subject<RestrictionalCircle>();
        public Subject<RestrictionalCircle> CircleEnter { get; } = new Subject<RestrictionalCircle>();

        public CirclesRestrictionInfoService(RestrictionalCircle[] restrictionalCircles)
        {
            _restrictionalCircles = restrictionalCircles;

            foreach (RestrictionalCircle restrictionalCircle in _restrictionalCircles)
            {
                restrictionalCircle.CircleExit.Subscribe((OnCircleExit)).AddTo(_compositeDisposable);
                restrictionalCircle.CircleEnter.Subscribe((OnCircleEnter)).AddTo(_compositeDisposable);
            }
        }

        private void OnCircleEnter(RestrictionalCircle circle)
        {
            CircleEnter.OnNext(circle);
        }

        private void OnCircleExit(RestrictionalCircle circle)
        {
            CircleExit.OnNext(circle);
        }

        public float GetRadius(int level)
        {
            int nextIndex = level;
            nextIndex = Mathf.Clamp(nextIndex, 1, 3);

            RestrictionalCircle circle = _restrictionalCircles.FirstOrDefault(x => x.HeightLevel == nextIndex);

            float radius;
            
            if (circle == null)
            {
                radius = 50; // default value if there is any mistake
                Debug.LogError($"Circles placed wrong");
            }
            else
            {
                radius = circle.GetRadius();
            }

            return radius;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}