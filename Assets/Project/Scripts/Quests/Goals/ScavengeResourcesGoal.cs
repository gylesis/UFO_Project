using Project.Buildings;
using UnityEngine;

namespace Project.Quests.Goals
{
    [CreateAssetMenu(menuName = "Quests/New ScavengeResources goal", fileName = "ScavengeResourcesGoal", order = 0)]
    public class ScavengeResourcesGoal : QuestGoal
    {
        public bool AnyResourceType;
        public BuildingResourceType ResourceType;
    }
}