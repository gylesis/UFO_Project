using UnityEngine;
using Zenject;

namespace Project.AI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private Config _config;
        [SerializeField] private StaticData _staticData;

        public override void InstallBindings()
        {
            Container.Bind<StaticData>().FromInstance(_staticData).AsSingle();
            Container.Bind<Config>().FromInstance(_config).AsSingle();

            Container.Bind<FXManager>().AsSingle();
        }
    }
}