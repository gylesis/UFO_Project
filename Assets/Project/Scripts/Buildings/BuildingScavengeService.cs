using Project.PlayerLogic;
using UniRx;
using UnityEngine;

namespace Project.Buildings
{
    public class BuildingScavengeService
    {
        private UpgradesService _upgradesService;

        public Subject<BuildingScavengeEventContext> ResourcesScavenged { get; } =
            new Subject<BuildingScavengeEventContext>();

        public BuildingScavengeService(UpgradesService upgradesService)
        {
            _upgradesService = upgradesService;
        }

        public bool AbleToScavenge(Building building)
        {
            return building.Data.BuildingResourcesData.Resource > 0;
        }

        public void Scavenge(Building building)
        {
            int amountToScavenge = (int) (building.Data.Height * (10));
            BuildingResourceType resourceType = building.Data.BuildingResourcesData.ResourceType;

            var resources = building.Data.BuildingResourcesData.Resource;
            
            if (amountToScavenge >= resources)
            {
                amountToScavenge = resources;
            }
            
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