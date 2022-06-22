using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.AI
{
    public class MovableEntitiesWatcher : IDisposable, ITickable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private readonly Dictionary<IMovableEntity, List<IMovingPattern>> _patterns = new Dictionary<IMovableEntity, List<IMovingPattern>>();
        private readonly IMovableEntity[] _movableEntities;

        public void Watch(IMovableEntity movableEntity)
        {
            Observable.Interval(GetRandomTime(15, 40)).Subscribe((_ => SwapPattern(movableEntity))).AddTo(_compositeDisposable);

            List<IMovingPattern> patterns = new List<IMovingPattern>();
            patterns.Add(movableEntity.MovingPattern);

            var movingPatterns = new List<IMovingPattern>();
            
            movingPatterns.Add(new StraightMoving());
            movingPatterns.Add(new BackwardMoving());
            movingPatterns.Add(new SinMoving());    

            patterns = movingPatterns.Except(patterns).ToList();

            _patterns.Add(movableEntity, patterns);
        }

        private TimeSpan GetRandomTime(int first, int second)
        {
            var seconds = Random.Range(first, second);

            return TimeSpan.FromSeconds(seconds);
        }

        private void SwapPattern(IMovableEntity movableEntity)
        {
            IMovingPattern movingPattern = GetPattern(movableEntity);
            movingPattern.CurrentAngle = movableEntity.MovingPattern.CurrentAngle;
            
            movableEntity.ChangeMovingPattern(movingPattern);
        }

        private IMovingPattern GetPattern(IMovableEntity movableEntity)
        {
            var movingPatterns = _patterns[movableEntity];
            IMovingPattern movingPattern = movingPatterns.FirstOrDefault(x => x != movableEntity.MovingPattern);
            return movingPattern;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        public void Tick()
        {
            foreach (IMovableEntity movableEntity in _patterns.Keys)
            {
                movableEntity.MoveTick();
            }
        }
    }
}