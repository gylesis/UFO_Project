using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Quests
{
    public class ReachGoalsConditionComposer : MonoBehaviour
    {
        [SerializeField] private List<ReachGoalConditionPair> _pairs;

        private Dictionary<QuestGoal, QuestConditionProcessor> _processors =
            new Dictionary<QuestGoal, QuestConditionProcessor>();

        private void Start()
        {
            var motherBase = FindObjectOfType<MotherBase>();
            
            foreach (ReachGoalConditionPair reachGoalConditionPair in _pairs)
            {
                ReachDestinationQuestGoal goal = reachGoalConditionPair.Goal;
                QuestTrigger questTrigger = reachGoalConditionPair.Trigger;

                var condition = new ReachDestinationCondition(questTrigger,goal);
                
                if (goal.IsMotherBase)
                    motherBase.StickService.Stick(questTrigger.transform);
                
                condition.StopProcessing();
                
                _processors.Add(goal,condition);
            }
        }

        public void StartConditionProcessing(QuestGoal targetGoal)
        {
            foreach (var questConditionProcessor in _processors)
            {
                if (questConditionProcessor.Key == targetGoal)
                {
                    Debug.Log($"Started condition processing for goal {targetGoal.GoalData.Title}");
                    _processors[targetGoal].StartProcessing();
                    return;
                }
            }
        }

        public void StopConditionProcessing(QuestGoal goal)
        {
            foreach (var questConditionProcessor in _processors)
            {
                if (questConditionProcessor.Key == goal)
                {
                    Debug.Log($"Stopped condition processing for goal {goal.GoalData.Title}");
                    _processors[goal].StopProcessing();
                }
            }
        }
        
    }

    [Serializable]
    public struct ReachGoalConditionPair
    {
        public ReachDestinationQuestGoal Goal;
        public QuestTrigger Trigger;
    }
}