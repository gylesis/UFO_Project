using UnityEngine;

namespace Project.Scripts.AI
{
    [CreateAssetMenu(menuName = "Quests/QuestsContainer", fileName = "QuestsContainer", order = 0)]
    public class QuestsContainer : ScriptableObject
    {
        [SerializeField] private Quest[] _quests;
        
        
        
    }
}