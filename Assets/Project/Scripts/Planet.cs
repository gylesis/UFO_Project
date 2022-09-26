using UnityEngine;

namespace Project
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        public Transform Pivot => _pivot;
    }
}