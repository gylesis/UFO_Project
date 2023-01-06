using UnityEngine;

namespace Project.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingView _buildingView;
        [SerializeField] private Transform _maxHeightPoint;
        [SerializeField] private BuildingScavengeTrigger _scavengeTrigger;
        [SerializeField] private BuildingResourceType _resourceType;

        public BuildingData Data { get; private set; }
        public BuildingScavengeTrigger ScavengeTrigger => _scavengeTrigger;
        public Transform MaxHeightPoint => _maxHeightPoint;
        public BuildingView BuildingView => _buildingView;
        public BuildingResourceType ResourceType => _resourceType;

        public void UpdateColor()
        {
            Color color = Color.blue;

            switch (_resourceType)
            {
                case BuildingResourceType.Blue:
                    color = Color.blue;
                    break;
                case BuildingResourceType.Yellow:
                    color = Color.yellow;
                    break;
                case BuildingResourceType.Red:
                    color = Color.red;
                    break;
            }

            _buildingView.SetSpriteColor(color);
        }

        public void Setup(BuildingData buildingData)
        {
            Data = buildingData;
        }

        public void UploadResourcesData(int currentResources)
        {
            Data.BuildingResourcesData.Resource = currentResources;

            _buildingView.Bar.UpdateValue((float)currentResources / Data.BuildingResourcesData.MaxResources);
        }
    }
}