using UniRx;
using UnityEngine;

namespace Project.Buildings
{
    public class BuildingScavengeService
    {
        public Subject<BuildingScavengeEventContext> ResourcesScavenged { get; } =
            new Subject<BuildingScavengeEventContext>();


        public bool AbleToScavenge(Building building)
        {
            return building.Data.BuildingResourcesData.Resource > 0;
        }

        public void Scavenge(Building building)
        {
            int amountToScavenge = (int) building.Data.Height * 10;
            BuildingResourceType resourceType = building.Data.BuildingResourcesData.ResourceType;

            var scavengeEventContext = new BuildingScavengeEventContext();

            scavengeEventContext.Building = building;
            scavengeEventContext.ResourcesTaken = amountToScavenge;
            scavengeEventContext.ResourceType = resourceType;

            Debug.Log($"Scavenged {amountToScavenge} from {building}, type - {resourceType}");

            ResourcesScavenged.OnNext(scavengeEventContext);
        }
    }


    public struct BuildingScavengeEventContext
    {
        public Building Building;
        public int ResourcesTaken;
        public BuildingResourceType ResourceType;
    }
}