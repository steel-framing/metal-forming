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

        public IList<Material> ListarMateriales(string codigo, string descripcion, int tipo, int min, int max, int estado)
        {
            return _materialBL.ListarMateriales(codigo, descripcion, tipo, min, max, estado);
        }

        public string GuardarMaterial(Material material)
        {
            return _materialBL.GuardarMaterial(material);
        }

        public string ActualizarMaterial(Material material)
        {
            return _materialBL.ActualizarMaterial(material);
        }

        public string EliminarMaterial(int id)
        {
            return _materialBL.EliminarMaterial(id);
        }
    }
}
