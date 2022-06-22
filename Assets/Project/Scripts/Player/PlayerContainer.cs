using System;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerContainer
    {
        public Transform Transform => _player.Transform;

        private readonly Player _player;

        public Player Player => _player;

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
            _player.CollisionCollider.enabled = false;

            _player.PlayerView.ColorUFO(Color.gray);
            Observable.Timer(timeSpan).TakeUntilDestroy(_player.Transform).Subscribe((l => MakeVulnerable()));
        }
        
        public void MakeVulnerable()
        {
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