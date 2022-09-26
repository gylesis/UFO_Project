using System;
using Project.Buildings;
using Project.Quests.Goals;
using UniRx;
using UnityEngine;

namespace Project.Quests.QuestsConditions
{
    public class ScavengeResourcesCondition : QuestConditionProcessor
    {
        private readonly BuildingScavengeService _buildingScavengeService;

        private IDisposable _disposable;
        private ScavengeResourcesGoal _goal;
        private BuildingsScavengingController _buildingsScavengingController;

        public ScavengeResourcesCondition(BuildingsScavengingController buildingsScavengingController,
            BuildingScavengeService buildingScavengeService, ScavengeResourcesGoal questGoal)
        {
            _buildingsScavengingController = buildingsScavengingController;
            _goal = questGoal;
            _buildingScavengeService = buildingScavengeService;
        }

        public override void StartProcessing()
        {
            Debug.Log("started processing");

            _disposable = _buildingScavengeService.ResourcesScavenged.Subscribe((context =>
            {
                var scavenged = 0;

                if (_goal.AnyResourceType)
                {
                    scavenged += context.ResourcesTaken;
                }
                else
                {
                    if (context.ResourceType == _goal.ResourceType)
                    {
                        scavenged += context.ResourcesTaken;
                    }
                }

                _goal.Process(scavenged);
            }));
        }

        public override void StopProcessing()
        {
            _buildingsScavengingController.StopScavenging();
            _disposable?.Dispose();
        }
    }
}