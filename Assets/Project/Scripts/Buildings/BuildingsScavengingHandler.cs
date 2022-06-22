using System;
using UniRx;

namespace Project.Scripts.Buildings
{
    public class BuildingsScavengingHandler : IDisposable
    {
        private readonly BuildingScavengingService _buildingScavengingService;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public BuildingsScavengingHandler(BuildingScavengeTrigger[] scavengeTriggers, BuildingScavengingService buildingScavengingService)
        {
            _buildingScavengingService = buildingScavengingService;
            
            foreach (BuildingScavengeTrigger buildingScavengeTrigger in scavengeTriggers)
            {
                buildingScavengeTrigger.ScavengeStart.Subscribe(OnBuildingScavenge).AddTo(_compositeDisposable);
                buildingScavengeTrigger.Exit.Subscribe((OnTriggerExit)).AddTo(_compositeDisposable);
            }
        }

        private void OnTriggerExit(Building building) => 
            _buildingScavengingService.StopScavenging(building);

        private void OnBuildingScavenge(Building building) => 
            _buildingScavengingService.StartScavenging(building);

        public void Dispose() => 
            _compositeDisposable.Dispose();
    }


    public class BuildingResource
    {
        
    }
    
}