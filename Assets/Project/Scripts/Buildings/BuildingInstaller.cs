using UnityEngine;
using Zenject;

namespace Project.Scripts.Buildings
{
    public class BuildingInstaller : MonoInstaller
    {
        [SerializeField] private Building _building;

        [Inject]
        private void Init(BuildingsRegistrationService buildingsRegistrationService)
        {
            buildingsRegistrationService.Register(_building);
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Building>().FromInstance(_building);
        }
    }
}