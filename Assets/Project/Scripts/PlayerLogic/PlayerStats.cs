using System;
using UnityEngine;

namespace Project.PlayerLogic
{
    [Serializable]
    public struct PlayerStats
    {
        [SerializeField] private int _healthCount;
        public int HealthCount => _healthCount;
    }
}