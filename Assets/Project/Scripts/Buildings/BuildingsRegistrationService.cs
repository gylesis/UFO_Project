using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Player;
using Zenject;

namespace Project.Scripts.Buildings
{
    public class BuildingsRegistrationService : IInitializable
    {
        private List<Building> _buildings = new List<Building>();
        private HeightRestrictService _heightRestrictService;
        private CoordinatesService _coordinatesService;
        private CirclesRestrictionInfoService _circlesRestrictionInfoService;

        [Inject]
        private void Init(HeightRestrictService heightRestrictService, CoordinatesService coordinatesService, CirclesRestrictionInfoService circlesRestrictionInfoService)
        {
            _circlesRestrictionInfoService = circlesRestrictionInfoService;
            _coordinatesService = coordinatesService;
            _heightRestrictService = heightRestrictService;
        }
        
        public void Register(Building building)
        {
            _buildings.Add(building);
        }

        public void Initialize()
        {
            var buildingDatas = _buildings.Select(x => x.Data);

            var orderedEnumerable = buildingDatas.OrderByDescending(x => x.Height);
            
            var data = orderedEnumerable.First();

            Building building = _buildings.First(x => x.Data == data);

            var radius = _coordinatesService.GetRadius(building.MaxHeightPoint.position);

            radius += 1;

            var thirdRadius = _circlesRestrictionInfoService.GetRadius(3);

            _heightRestrictService.SetMaxHeight(thirdRadius - 3);
            _heightRestrictService.SetMinHeight(radius);
        }
    }
}