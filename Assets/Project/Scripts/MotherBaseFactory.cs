using Zenject;

namespace Project.Scripts
{
    public class MotherBaseFactory : PlaceholderFactory<MotherBaseSpawnContext, MotherBase>
    {
        private readonly CoordinatesService _coordinatesService;

        public MotherBaseFactory(CoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
        }
        
        public override MotherBase Create(MotherBaseSpawnContext context)
        {
            MotherBase motherBase = base.Create(context);
            
            _coordinatesService.SetPolarCoords(motherBase.transform, context.Pos);
            
            return motherBase;
        }
    }
}