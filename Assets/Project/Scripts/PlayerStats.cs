using System;
using UnityEngine;

namespace Project
{
    [Serializable]
    public struct PlayerStats
    {
        [SerializeField] private int _healthCount;
        public int HealthCount => _healthCount;
    }
}