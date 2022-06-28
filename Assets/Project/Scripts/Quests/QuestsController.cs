using System;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Quests
{
    public class QuestsController : ITickable, IDisposable
    {
        private readonly QuestsContainer _questsContainer;
        private readonly QuestView _questView;

        private Quest _currentQuest;
        private QuestGoal _currentGoal;

        private ReachGoalsConditionComposer _reachGoalsConditionComposer;

        public QuestsController(QuestsContainer questsContainer, QuestView questView,
            ReachGoalsConditionComposer reachGoalsConditionComposer)
        {
            _reachGoalsConditionComposer = reachGoalsConditionComposer;
            _questView = questView;
            _questsContainer = questsContainer;
        }

        public void StartNextQuest()
        {
            if (_currentQuest != null)
            {
                if (_currentQuest.IsFinished == false)
                {
                    Debug.Log("zakonchi proshlii qvest");
                    return;
                }
            }

            var doesExistQuests = _questsContainer.DoExistQuests();

            if (doesExistQuests)
            {
                Quest nextQuest = _questsContainer.GetNextQuest();
                _currentQuest = nextQuest;

                nextQuest.GoalChanged += OnQuestGoalChanged;
                nextQuest.Completed += OnQuestCompleted;
                nextQuest.Start();
                
                _questView.SetQuest(nextQuest);
                _questView.PullContainer();
            }
        }

        private void OnQuestCompleted(Quest quest)
        {
            _questView.OnQuestComplete(quest);
            _questView.PullContainer(TimeSpan.FromSeconds(1));

            quest.Completed += OnQuestCompleted;
            _reachGoalsConditionComposer.StopConditionProcessing(_currentGoal);

            if (_questsContainer.DoExistQuests() == false)
            {
                _questView.OnQuestGoalChanged(null);
            }
            
        }

        private void OnQuestGoalChanged(QuestGoal goal)
        {
            if (_currentGoal != null)
                _reachGoalsConditionComposer.StopConditionProcessing(_currentGoal);

            _questView.OnQuestGoalChanged(goal);
            _questView.PullContainer(TimeSpan.FromSeconds(3));
            
            _reachGoalsConditionComposer.StartConditionProcessing(goal);

            _currentGoal = goal;
        }

        public void Tick()
        {
            if (Input.touches.Length == 3)
            {
                StartNextQuest();
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                StartNextQuest();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                _questView.PullContainer();
            }
        }

        public void Dispose()
        {
            if (_currentQuest != null)
            {
                _currentQuest.Dispose();
                _currentQuest.GoalChanged -= OnQuestGoalChanged;
                _reachGoalsConditionComposer.StopConditionProcessing(_currentGoal);
            }
        }
    }
}