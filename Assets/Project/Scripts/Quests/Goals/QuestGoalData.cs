using System;
using UnityEngine;

namespace Project.Scripts.Quests
{
    [Serializable]
    public class QuestGoalData
    {
        [SerializeField] private int _id;
        [SerializeField] private string _title = "----- Title here -----";
        [TextArea] [SerializeField] private string _description = "----- Description here -----";
        [Min(1)][SerializeField] private int _amount;

        public int ID => _id;
        public string Title => _title;
        public string Description => _description;
        public int Amount => _amount;
        
        public int CurrentAmount { get; set; }

    }
}