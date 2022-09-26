using UnityEngine;

namespace Project.AI
{
    [CreateAssetMenu(menuName = "StaticData/StaticData", fileName = "StaticData", order = 0)]
    public class StaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject MissileExplosionParticles { get; set; }
    }
}