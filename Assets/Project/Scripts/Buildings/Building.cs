using UnityEngine;
using Zenject;

namespace Project.Scripts.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingView _buildingView;
        [SerializeField] private Transform _maxHeightPoint;
        [SerializeField] private BuildingScavengeTrigger _scavengeTrigger;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private float _height;
        
        private CoordinatesService _coordinatesService;
        public BuildingData Data { get; private set; }
        public BuildingScavengeTrigger ScavengeTrigger => _scavengeTrigger;

        public Transform MaxHeightPoint => _maxHeightPoint;
        
        [Inject]
        private void Init(CoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
            float height = GetHeightOfBuilding();
            _height = height;
            
            var buildingResourcesData = new BuildingResourcesData();
            buildingResourcesData.Tenge = 2000;
            
            Data = new BuildingData(height, buildingResourcesData);
        }

        private float GetHeightOfBuilding()
        {
            var radius = _coordinatesService.GetRadius(_maxHeightPoint.position);

            return radius;
        }
    }

    public class BuildingResourcesData
    {
        public int Tenge;
    }
    
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