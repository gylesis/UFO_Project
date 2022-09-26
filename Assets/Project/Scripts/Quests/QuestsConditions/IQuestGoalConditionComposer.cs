using System.Collections.Generic;
using Project.Quests.Goals;

namespace Project.Quests.QuestsConditions
{
    public interface IQuestGoalConditionComposer
    {
        Dictionary<QuestGoal, QuestConditionProcessor> Processors { get; }
    }
}