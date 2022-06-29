using Project.Scripts.Player;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Buildings
{
    public class BuildingScavengeService
    {
        private readonly PlayerWallet _playerWallet;
        public Subject<BuildingScavengeEventContext> ResourcesScavenged { get; } =
            new Subject<BuildingScavengeEventContext>();
        
        public BuildingScavengeService(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }

        public bool AbleToScavenge(Building building)
        {
            return building.Data.BuildingResourcesData.Resource > 0;
        }
        
        public void Scavenge(Building building)
        {
            int amountToScavenge = (int) building.Data.Height * 10;
            BuildingResourceType buildingResourceType = building.Data.BuildingResourcesData.ResourceType;

            building.Data.BuildingResourcesData.Resource -= amountToScavenge;

            building.Data.BuildingResourcesData.Resource =
                Mathf.Clamp(building.Data.BuildingResourcesData.Resource, 0, int.MaxValue);

            var scavengeEventContext = new BuildingScavengeEventContext();

            scavengeEventContext.Building = building;
            scavengeEventContext.ResourcesTaken = amountToScavenge;
            scavengeEventContext.ResourceType = buildingResourceType;

            Debug.Log($"Scavenged {amountToScavenge} from {building}, type - {buildingResourceType}");
            
            ResourcesScavenged.OnNext(scavengeEventContext);
            
            _playerWallet.AddTenge(amountToScavenge);
        }
    }

    public struct BuildingScavengeEventContext
    {
        public Building Building;
        public int ResourcesTaken;
        public BuildingResourceType ResourceType;
    }
    
}