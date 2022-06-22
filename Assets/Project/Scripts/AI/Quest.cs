using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Project.Scripts.AI
{
    [CreateAssetMenu(menuName = "Quests/New Quest", fileName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private QuestGoal[] _goals;

        public Subject<Quest> Completed { get; } = new Subject<Quest>();

        public void Process(int amount)
        {
            QuestGoal goal = _goals.FirstOrDefault(x => x.IsFinished == false);

            if (goal == null)
            {
                FinishQuest();
                return;
            }

            goal.GoalData.CurrentAmount += amount;

            if (goal.GoalData.CurrentAmount >= goal.GoalData.Amount)
            {
                FinishQuest();
            }
        }

        private void FinishQuest()
        {
            Completed.OnNext(this);
        }
        
    }


    [Serializable]
    public class QuestGoalData
    {
        public int ID;
        public string Title = "----- Title here -----";
        [TextArea] public string Description = "----- Description here -----";
        public int Amount;
        [HideInInspector] public int CurrentAmount;
    }
}