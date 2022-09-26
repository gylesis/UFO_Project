using System.Collections.Generic;
using System.Linq;
using Project.Quests.Goals;
using UnityEngine;

namespace Project.Quests
{
    [CreateAssetMenu(menuName = "Quests/QuestsContainer", fileName = "QuestsContainer", order = 0)]
    public class QuestsContainer : ScriptableObject
    {
        [SerializeField] private Quest[] _quests;

        public Quest GetQuestByGoal(QuestGoal goal)
        {
            foreach (Quest quest in _quests)
            {
                var containsGoal = quest.DoContainsGoal(goal);

                if (containsGoal)
                    return quest;
            }

            return null;
        }

        public TGoalType[] GetAllGoalsByType<TGoalType>() where TGoalType : QuestGoal
        {
            List<TGoalType> goals = new List<TGoalType>();

            foreach (Quest quest in _quests)
            {
                foreach (QuestGoal goal in quest.Goals)
                {
                    if (goal is TGoalType foundedGoal)
                    {
                        goals.Add(foundedGoal);
                    }
                }
            }

            return goals.ToArray();
        }

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