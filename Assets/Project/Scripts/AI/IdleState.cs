using System;
using UniRx;
using UnityEngine;

namespace Project.AI
{
    public class IdleState : IState
    {
        private AntiAirDefense _airDefense;
        private IDisposable _disposable;

        public IdleState(AntiAirDefense airDefense)
        {
            _airDefense = airDefense;
        }

        public void Enter()
        {
            _disposable = Observable.EveryUpdate().Subscribe((l =>
            {
                Vector3 quaternion = _airDefense.Krutilka.rotation.eulerAngles;

                quaternion.z += 1;

                _airDefense.Krutilka.rotation = Quaternion.Euler(quaternion);
            }));
        }

        public void Exit()
        {
            _disposable.Dispose();
        }
    }
}