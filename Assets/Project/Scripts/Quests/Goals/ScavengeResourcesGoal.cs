using Project.Scripts.Buildings;
using UnityEngine;

namespace Project.Scripts.Quests
{
    [CreateAssetMenu(menuName = "Quests/New ScavengeResources goal", fileName = "ScavengeResourcesGoal", order = 0)]
    public class ScavengeResourcesGoal : QuestGoal
    {
        public bool AnyResourceType;
        public BuildingResourceType ResourceType;
    }
}