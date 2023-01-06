using Project.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class UpgradesInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeView _upgradeViewPrefab;
        [SerializeField] private UpgradeUIViewData _upgradeUIViewData;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UpgradesService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<UpgradesViewController>().AsSingle().WithArguments(_upgradeUIViewData, _upgradeViewPrefab);
        }
    }
}