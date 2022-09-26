using System;
using UniRx;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerContainer
    {
        public Transform Transform => _player.Transform;

        private readonly Player _player;

        public Player Player => _player;

        public bool IsInvulnerable { get; private set; }

        public PlayerContainer(Player player)
        {
            _player = player;
        }

        public void MakeInvulnerable()
        {
            _player.CollisionCollider.enabled = false;

            _player.PlayerView.ColorUFO(Color.gray);
        }

        public void MakeInvulnerable(TimeSpan timeSpan)
        {
            IsInvulnerable = true;
            _player.CollisionCollider.enabled = false;

            _player.PlayerView.ColorUFO(Color.gray);
            Observable.Timer(timeSpan).Subscribe((l => MakeVulnerable()));
        }

        public void MakeVulnerable()
        {
            IsInvulnerable = false;

            _player.CollisionCollider.enabled = true;
            _player.PlayerView.ColorDefault();
        }

        public void LerpTo(Transform target, Action onLerpEnded)
        {
            _player.PlayerController.StartLeprTo(target, onLerpEnded);
        }

        public void StopLerp()
        {
            _player.PlayerController.StopLerp();
        }
    }
}