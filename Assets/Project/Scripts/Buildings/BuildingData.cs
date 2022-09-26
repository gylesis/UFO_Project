namespace Project.Buildings
{
    public struct BuildingData
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