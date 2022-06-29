namespace Project.Scripts.Buildings
{
    public class BuildingData
    {
        public float Height { get; }
        public BuildingResourcesData BuildingResourcesData { get; }
        public BuildingData(float height, BuildingResourcesData buildingResourcesData)
        {
            Height = height;
            BuildingResourcesData = buildingResourcesData;
        }
    }
}