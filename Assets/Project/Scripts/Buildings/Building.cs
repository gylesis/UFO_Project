using System;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingView _buildingView;
        [SerializeField] private Transform _maxHeightPoint;
        [SerializeField] private BuildingScavengeTrigger _scavengeTrigger;
        [SerializeField] private BuildingResourceType _buildingResourceType;
        
        private CoordinatesService _coordinatesService;
        public BuildingData Data { get; private set; }
        public BuildingScavengeTrigger ScavengeTrigger => _scavengeTrigger;
        public Transform MaxHeightPoint => _maxHeightPoint;
        
        [Inject]
        private void Init(CoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
            
            float height = GetHeightOfBuilding();
            
            var buildingResourcesData = new BuildingResourcesData();
            buildingResourcesData.Resource = 2000;
            buildingResourcesData.ResourceType = _buildingResourceType;

            Color color = Color.blue;

            switch (_buildingResourceType)
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
            
            Data = new BuildingData(height, buildingResourcesData);
        }
       
        private float GetHeightOfBuilding()
        {
            var radius = _coordinatesService.GetRadius(_maxHeightPoint.position);

            return radius;
        }
    }
}