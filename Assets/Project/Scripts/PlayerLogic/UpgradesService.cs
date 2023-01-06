using System;
using System.Collections.Generic;
using Project.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class UpgradesService : IInitializable, IDisposable
    {
        private SaveDataLoadAndSaveService _saveDataLoadAndSaveService;
        private UpgradesViewController _upgradesViewController;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private Dictionary<UpgradeType, ushort> _upgradesLevel = new Dictionary<UpgradeType, ushort>();

        public UpgradesService(SaveDataLoadAndSaveService saveDataLoadAndSaveService, UpgradesViewController upgradesViewController)
        {
            _upgradesViewController = upgradesViewController;
            _saveDataLoadAndSaveService = saveDataLoadAndSaveService;
        }

        public void Initialize()
        {
            _upgradesViewController.UpgradeEvent.Subscribe((OnTryToUpgrade)).AddTo(_compositeDisposable);
            _upgradesLevel.Add(UpgradeType.ScavengeSpeed, 0);
            
            var upgradeUIViewContext = new UpgradeUIViewContext();
            
            upgradeUIViewContext.Cost = 100;    
            upgradeUIViewContext.Description = "COOL DESCRIPTION";
            upgradeUIViewContext.Level = 1;
            upgradeUIViewContext.Name = $"SCAVENGE SPEED";
            upgradeUIViewContext.UpgradeType = UpgradeType.ScavengeSpeed;
            
            _upgradesViewController.SpawnView(upgradeUIViewContext);
        }

        private void OnTryToUpgrade(UpgradeType upgradeType)
        {
            Debug.Log($"Clicked to upgrade {upgradeType}");

            ushort upgradeLevel = _upgradesLevel[upgradeType];
            
            switch (upgradeType)
            {
                case UpgradeType.Health:
                    break;
                case UpgradeType.ScavengeSpeed:
                    upgradeLevel++;

                    _upgradesLevel[upgradeType] = upgradeLevel;
                    break;
                case UpgradeType.FlySpeed:
                    break;
                case UpgradeType.Shield:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null);
            }

            UpdateView(upgradeType, upgradeLevel);
        }

        private void UpdateView(UpgradeType upgradeType, ushort newLevel)
        {
            var updateContext = new UpdateViewUpdateContext();
            updateContext.NewLevel = newLevel;
            updateContext.UpgradeType = upgradeType;
            updateContext.NewPrice = newLevel * 100 * 2;
            
            _upgradesViewController.UpdateView(updateContext);
        }
        
        public float GetSpeedMultiplier(int heightLevel)
        {
            heightLevel = Mathf.Clamp(heightLevel, 1, 3);

            float multiplier = 1;
            
            switch (heightLevel)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }

            return multiplier;
        }

        public float GetScavengeSpeedMultiplier() 
        {
            var state = _upgradesLevel[UpgradeType.ScavengeSpeed];

            float multiplier;
            
            switch (state) // temporary, need to put this in SO
            {
                case 0:
                    multiplier = 1f;
                    break;
                
                case 1:
                    multiplier = 1.1f;
                    break;
                    
                case 2:
                    multiplier = 1.2f;
                    break;
                case 3:
                    multiplier = 1.3f;
                    break;
                default:
                    multiplier = 1.3f;
                    break;
            }
            
            return multiplier;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}