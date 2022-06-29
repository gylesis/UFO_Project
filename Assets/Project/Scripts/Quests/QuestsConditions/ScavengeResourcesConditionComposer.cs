using System;
using System.Collections.Generic;
using Project.Scripts.Buildings;
using Zenject;

namespace Project.Scripts.Quests
{
    public class ScavengeResourcesConditionComposer : IInitializable, IQuestGoalConditionComposer
    {
        private readonly BuildingScavengeService _buildingScavengeService;
        private readonly QuestsContainer _questsContainer;
        private readonly BuildingsScavengingController _buildingsScavengingController;
        public Dictionary<QuestGoal, QuestConditionProcessor> Processors => _processors;

        private Dictionary<QuestGoal, QuestConditionProcessor> _processors =
            new Dictionary<QuestGoal, QuestConditionProcessor>();

        private ScavengeResourcesConditionComposer(BuildingScavengeService buildingScavengeService,
            QuestsContainer questsContainer, BuildingsScavengingController buildingsScavengingController)
        {
            _buildingsScavengingController = buildingsScavengingController;
            _questsContainer = questsContainer;
            _buildingScavengeService = buildingScavengeService;
        }

        public void Initialize()
        {
            var goals = _questsContainer.GetAllGoalsByType<ScavengeResourcesGoal>();

            foreach (ScavengeResourcesGoal goal in goals)
            {
                var scavengeResourcesCondition =
                    new ScavengeResourcesCondition(_buildingsScavengingController, _buildingScavengeService, goal);

                _processors.Add(goal, scavengeResourcesCondition);
            }
        }
    }
}