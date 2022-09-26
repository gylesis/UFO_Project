using Project.Quests.Goals;

namespace Project.Quests.QuestsConditions
{
    public class QuestGoalsConditionResolver
    {
        private readonly IQuestGoalConditionComposer[] _conditionComposers; // 2 штуки ресуры и долететь до
        private QuestsContainer _questsContainer;

        public QuestGoalsConditionResolver(IQuestGoalConditionComposer[] conditionComposers,
            QuestsContainer questsContainer)
        {
            _questsContainer = questsContainer;
            _conditionComposers = conditionComposers;
        }

        public void StartConditionProcessing(QuestGoal goal)
        {
            Quest quest = _questsContainer.GetQuestByGoal(goal);

            foreach (IQuestGoalConditionComposer conditionComposer in _conditionComposers)
            {
                if (conditionComposer.Processors.ContainsKey(goal) == false) continue;

                foreach (QuestGoal questGoal in conditionComposer.Processors.Keys)
                {
                    if (quest.DoContainsGoal(questGoal))
                    {
                        conditionComposer.Processors[goal].StartProcessing();
                        return;
                    }
                }
            }
        }

        public void StopConditionProcessing(QuestGoal goal)
        {
            Quest quest = _questsContainer.GetQuestByGoal(goal);

            foreach (IQuestGoalConditionComposer conditionComposer in _conditionComposers)
            {
                if (conditionComposer.Processors.ContainsKey(goal) == false) continue;

                foreach (QuestGoal questGoal in conditionComposer.Processors.Keys)
                {
                    if (quest.DoContainsGoal(questGoal))
                    {
                        conditionComposer.Processors[goal].StopProcessing();
                    }
                }
            }
        }
    }
}