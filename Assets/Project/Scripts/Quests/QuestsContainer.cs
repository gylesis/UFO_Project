using System.Linq;
using UnityEngine;

namespace Project.Scripts.Quests
{
    [CreateAssetMenu(menuName = "Quests/QuestsContainer", fileName = "QuestsContainer", order = 0)]
    public class QuestsContainer : ScriptableObject
    {
        [SerializeField] private Quest[] _quests;

        public Quest[] Quests => _quests;

        public Quest GetNextQuest()
        {
            return _quests.FirstOrDefault(x => x.IsFinished == false);
        }
        
        public bool DoExistQuests()
        {
            return _quests.Any(x => x.IsFinished == false);
        }
        
    }
}