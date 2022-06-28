namespace Project.Scripts.Quests
{
    public abstract class QuestConditionProcessor
    {
        protected QuestGoal _goal;

        public QuestConditionProcessor(QuestGoal goal)
        {
            _goal = goal;
        }
        public abstract void StartProcessing();

        public abstract void StopProcessing();
        protected void ProcessGoal()
        {
            _goal.Process();
        }
        
    }
}