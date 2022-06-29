using System;
using UniRx;

namespace Project.Scripts.Buildings
{
    public class BuildingsScavengingHandler : IDisposable
    {
        private readonly BuildingsScavengingController _buildingsScavengingController;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public BuildingsScavengingHandler(BuildingScavengeTrigger[] scavengeTriggers, BuildingsScavengingController buildingsScavengingController)
        {
            _buildingsScavengingController = buildingsScavengingController;
            
            foreach (BuildingScavengeTrigger buildingScavengeTrigger in scavengeTriggers)
            {
                buildingScavengeTrigger.ScavengeStart.Subscribe(OnBuildingScavenge).AddTo(_compositeDisposable);
                buildingScavengeTrigger.Exit.Subscribe((OnTriggerExit)).AddTo(_compositeDisposable);
            }
        }

        private void OnTriggerExit(Building building) => 
            _buildingsScavengingController.StopScavenging();

        private void OnBuildingScavenge(Building building) => 
            _buildingsScavengingController.StartPollingToScavenge(building);

        public void Dispose() => 
            _compositeDisposable.Dispose();
    }
}