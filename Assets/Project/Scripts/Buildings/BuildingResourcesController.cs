using System;
using System.Collections.Generic;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Buildings
{
    public class BuildingResourcesController : IInitializable, IDisposable
    {
        private Dictionary<int, IDisposable> _restoreResourcesCooldown = new Dictionary<int, IDisposable>();
        private Dictionary<int, bool> _availabilityToRestore = new Dictionary<int, bool>();

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private HashSet<Building> _buildings = new HashSet<Building>();
        private readonly PlayerWallet _playerWallet;
        private BuildingScavengeService _scavengeService;

        public BuildingResourcesController(BuildingScavengeService scavengeService, PlayerWallet playerWallet)
        {
            _scavengeService = scavengeService;
            _playerWallet = playerWallet;
        }

        public void Initialize()
        {
            _scavengeService.ResourcesScavenged.Subscribe((OnBuildingResourcesScavenged)).AddTo(_compositeDisposable);
        }

        private void OnBuildingResourcesScavenged(BuildingScavengeEventContext context)
        {
            Building building = context.Building;

            _playerWallet.AddTenge(context.ResourcesTaken);

            if (_buildings.Contains(building) == false)
            {
                _buildings.Add(building);
            }

            if (_restoreResourcesCooldown.ContainsKey(building.GetInstanceID()) == false)
            {
                _restoreResourcesCooldown.Add(building.GetInstanceID(), null);
            }

            _restoreResourcesCooldown[building.GetInstanceID()]?.Dispose();

            var disposable = Observable.Timer(TimeSpan.FromSeconds(3))
                .Concat(Observable.Interval(TimeSpan.FromSeconds(0.5f))).Subscribe((l => RestoreResources(building)));

            _restoreResourcesCooldown[building.GetInstanceID()] = disposable;

            if (_availabilityToRestore.ContainsKey(building.GetInstanceID()) == false)
            {
                _availabilityToRestore.Add(building.GetInstanceID(), false);
            }

            var currentResources = building.Data.BuildingResourcesData.Resource;

            currentResources -= context.ResourcesTaken;

            currentResources = Mathf.Clamp(currentResources, 0, building.Data.BuildingResourcesData.MaxResources);

            building.UploadResourcesData(currentResources);

            GG(building);
        }

        private void GG(Building building) { }

        private void RestoreResources(Building building)
        {
            var currentResources = building.Data.BuildingResourcesData.Resource;
            var maxResources = building.Data.BuildingResourcesData.MaxResources;

            currentResources += 100;

            currentResources = Mathf.Clamp(currentResources, 0, maxResources);

            building.UploadResourcesData(currentResources);

            if (currentResources >= maxResources)
            {
                _restoreResourcesCooldown[building.GetInstanceID()]?.Dispose();
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}