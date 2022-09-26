using System;
using Project.Quests.Goals;
using UniRx;

namespace Project.Quests.QuestsConditions
{
    public class ReachDestinationCondition : QuestConditionProcessor
    {
        private readonly QuestTrigger _questTrigger;
        private IDisposable _disposable;
        private ReachDestinationQuestGoal _goal;

        public ReachDestinationCondition(QuestTrigger questTrigger, ReachDestinationQuestGoal goal)
        {
            _goal = goal;
            _questTrigger = questTrigger;
        }

        public override void StartProcessing()
        {
            _questTrigger.gameObject.SetActive(true);
            _questTrigger.enabled = true;
            _disposable = _questTrigger.Triggered.Take(1).Subscribe((_ => _goal.Process()));
        }

        public override void StopProcessing()
        {
            if (_questTrigger.gameObject != null)
            {
                _questTrigger.enabled = false;
                _questTrigger.gameObject.SetActive(false);
            }

            _disposable?.Dispose();
        }
    }
}