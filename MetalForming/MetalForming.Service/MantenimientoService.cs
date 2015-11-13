using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic;
using MetalForming.ServiceContracts;
using System.Collections.Generic;
using System.ServiceModel.Activation;

namespace MetalForming.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MantenimientoService : IMantenimientoService
    {
        private readonly MaterialBL _materialBL = new MaterialBL();

        public IList<Material> ListarMateriales()
        {
            throw new System.NotImplementedException();
        }
    }
}
