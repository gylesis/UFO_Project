using System;
using System.Collections.Generic;
using Project.MotherbaseLogic;
using Project.Quests.Goals;
using UnityEngine;

namespace Project.Quests.QuestsConditions
{
    public class ReachDestinationGoalsConditionComposer : MonoBehaviour, IQuestGoalConditionComposer
    {
        [NonReorderable] [SerializeField] private List<ReachGoalConditionPair> _pairs;

        public Dictionary<QuestGoal, QuestConditionProcessor> Processors => _processors;

        private Dictionary<QuestGoal, QuestConditionProcessor> _processors =
            new Dictionary<QuestGoal, QuestConditionProcessor>();

        private void Start()
        {
            var motherBase = FindObjectOfType<MotherBase>();

            foreach (ReachGoalConditionPair reachGoalConditionPair in _pairs)
            {
                ReachDestinationQuestGoal goal = reachGoalConditionPair.Goal;
                QuestTrigger questTrigger = reachGoalConditionPair.Trigger;

                var condition = new ReachDestinationCondition(questTrigger, goal);

                if (goal is ReachMotherbaseQuestGoal)
                    motherBase.StickService.Stick(questTrigger.transform);

                condition.StopProcessing();

                _processors.Add(goal, condition);
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