using UniRx;

namespace Project.Scripts.Quests
{
    public class ReachDestinationCondition : QuestConditionProcessor
    {
        private readonly QuestTrigger _questTrigger;

        public ReachDestinationCondition(QuestTrigger questTrigger, ReachDestinationQuestGoal goal) : base(goal)
        {
            _questTrigger = questTrigger;
        }

        public override void StartProcessing()
        {
            _questTrigger.gameObject.SetActive(true);
            _questTrigger.enabled = true;
            _questTrigger.Triggered.Take(1).Subscribe((_ => ProcessGoal()));
        }

        public override void StopProcessing()
        {
            _questTrigger.enabled = false;
            _questTrigger.gameObject.SetActive(false);
        }
    }
    
}