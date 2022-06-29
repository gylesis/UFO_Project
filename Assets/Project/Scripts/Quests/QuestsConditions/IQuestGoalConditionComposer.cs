using System.Collections.Generic;

namespace Project.Scripts.Quests
{
    public interface IQuestGoalConditionComposer
    {
        Dictionary<QuestGoal, QuestConditionProcessor> Processors { get; }
    }
}