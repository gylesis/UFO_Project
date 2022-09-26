using Zenject;

namespace Project.Buildings
{
    public class BuildingsInitializeService : IInitializable
    {
        private Config _config;
        private CoordinatesService _coordinatesService;
        private Building[] _buildings;

        public BuildingsInitializeService(Config config, CoordinatesService coordinatesService, Building[] buildings)
        {
            _buildings = buildings;
            _coordinatesService = coordinatesService;
            _config = config;
        }

        public void Initialize()
        {
            foreach (Building building in _buildings)
            {
                Init(building);
            }
        }

        private void Init(Building building)
        {
            var height = GetHeightOfBuilding(building);

            var resourcesData = new BuildingResourcesData();

            resourcesData.Resource = _config.BuildingResourcesData.StartResources;
            resourcesData.MaxResources = _config.BuildingResourcesData.StartResources;
            resourcesData.ResourceType = building.ResourceType;

            var buildingData = new BuildingData(height, resourcesData);

            building.Setup(buildingData);

            building.UpdateColor();
        }

        private float GetHeightOfBuilding(Building building)
        {
            var radius = _coordinatesService.GetRadius(building.MaxHeightPoint.position);

            return radius;
        }
    }
}