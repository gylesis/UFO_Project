using UnityEngine;

namespace Project.Scripts.Quests
{
    [CreateAssetMenu(menuName = "Quests/New ReachDestination goal", fileName = "QuestGoal", order = 0)]
    public class ReachDestinationQuestGoal : QuestGoal
    {
        public bool IsMotherBase;
    }
    
}