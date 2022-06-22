using UnityEngine;

namespace Project.Scripts.AI
{
    public abstract class QuestGoal : ScriptableObject   
    {
        public QuestGoalData GoalData;
        [HideInInspector] public bool IsFinished;
    }
}