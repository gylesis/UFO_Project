using System;
using System.Collections.Generic;
using Project.Scripts.Camera;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Buildings
{
    public class BuildingScavengeConditionService
    {
        private readonly CameraContainer _cameraContainer;
        private readonly Transform _playerClaw;
        private readonly PlayerContainer _playerContainer;

        private readonly List<Func<bool>> _conditions = new List<Func<bool>>();

        public BuildingScavengeConditionService(PlayerContainer playerContainer, CameraContainer cameraContainer)
        {
            _playerContainer = playerContainer;
            _cameraContainer = cameraContainer;
            _playerClaw = playerContainer.Player.PlayerClaw.transform;

            AddCondition(IsPlayerHasToStopScavenging);
        }

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        private bool IsPlayerHasToStopScavenging()
        {
            var distance = (_playerClaw.transform.position - _playerContainer.Player.transform.position).sqrMagnitude;

            return distance > 40;
        }

        public bool Any()
        {
            foreach (var condition in _conditions)
            {
                var invoke = condition.Invoke();

                if (invoke)
                {
                    return true;
                }
            }

            return false;
        }
    }
}