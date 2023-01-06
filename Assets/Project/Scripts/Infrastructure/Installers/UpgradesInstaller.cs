using Project.PlayerLogic.Upgrades;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure.Installers
{
    public class UpgradesInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeView _upgradeViewPrefab;
        [SerializeField] private UpgradeUIViewData _upgradeUIViewData;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UpgradesService>().AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradesViewController>().AsSingle()
                .WithArguments(_upgradeUIViewData, _upgradeViewPrefab);
        }
    }
}