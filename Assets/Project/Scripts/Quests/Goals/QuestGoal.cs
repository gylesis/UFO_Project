using System;
using UnityEngine;

namespace Project.Scripts.Quests
{
    public abstract class QuestGoal : ScriptableObject   
    {
        [SerializeField] private QuestGoalData _goalData;
         
        private bool _isFinished;

        public bool IsFinished => _isFinished;
        public QuestGoalData GoalData => _goalData;

        public event Action<QuestGoal> Processed;
        public event Action<QuestGoal> Completed;
        
        public void Process(int value)
        {
            ProcessInternal(value);
        }
        
        public void Process()
        {
            ProcessInternal();
        }

        private void ProcessInternal(int value = 1)
        {
            Debug.Log( $"Goal: {_goalData.Title} processes by {value}");
            
            _goalData.CurrentAmount += value;
            _goalData.CurrentAmount = Mathf.Clamp(_goalData.CurrentAmount, 0, _goalData.Amount);

            Processed?.Invoke(this);
            
            if (_goalData.CurrentAmount >= _goalData.Amount)
            {
                _isFinished = true;
                Completed?.Invoke(this);
            }
        }

        public void Restart()
        {
            _isFinished = false;
            _goalData.CurrentAmount = 0;
        }
        
    }
}