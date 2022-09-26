using System;
using System.Linq;
using Project.Quests.Goals;
using UnityEngine;

namespace Project.Quests
{
    [CreateAssetMenu(menuName = "Quests/New Quest", fileName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private QuestGoal[] _goals;

        public QuestGoal[] Goals => _goals;

        private bool _isFinished;

        public event Action<Quest> Completed;
        public event Action<QuestGoal> GoalChanged;

        public string Title => _title;
        public bool IsFinished => _isFinished;
        public QuestGoal CurrentGoal => _goals.FirstOrDefault(x => x.IsFinished == false);

        public void Start()
        {
            QuestGoal firstGoal = _goals[0];

            GoalChanged?.Invoke(firstGoal);

            foreach (QuestGoal questGoal in _goals)
            {
                questGoal.Completed += OnGoalCompleted;
            }
        }

        public bool DoContainsGoal(QuestGoal goal)
        {
            return _goals.FirstOrDefault(x => x == goal) != null;
        }

        private void OnGoalCompleted(QuestGoal goal)
        {
            goal.Completed -= OnGoalCompleted;

            var areAllGoalsCompleted = _goals.All(x => x.IsFinished);

            if (areAllGoalsCompleted)
            {
                FinishQuest();
            }
            else
            {
                Debug.Log($"Goal {goal.GoalData.Title} completed");
                GoNextGoal();
            }
        }

        public void Process(QuestGoal goal)
        {
            QuestGoal goals = _goals.FirstOrDefault(x => x.GoalData.ID == goal.GoalData.ID);

            goal.Process();
        }

        private void GoNextGoal()
        {
            QuestGoal nextGoal = _goals.FirstOrDefault(x => x.IsFinished == false);
            GoalChanged?.Invoke(nextGoal);
        }

        private void FinishQuest()
        {
            Debug.Log($"Quest {_title} finished");
            _isFinished = true;
            Completed?.Invoke(this);
        }

        public void Dispose()
        {
            foreach (QuestGoal questGoal in _goals)
            {
                questGoal.Completed -= OnGoalCompleted;
            }

            RestartQuest();
        }

        [ContextMenu(nameof(RestartQuest))]
        public void RestartQuest()
        {
            _isFinished = false;

            foreach (QuestGoal questGoal in _goals)
            {
                questGoal.Restart();
            }
        }
    }
}