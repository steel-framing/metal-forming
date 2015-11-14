using System.Collections.Generic;
using System.ServiceModel;
using MetalForming.BusinessEntities;

namespace MetalForming.ServiceContracts
{
    [ServiceContract]
    public interface IMantenimientoService
    {
        [OperationContract]
        IList<Material> ListarMateriales(string codigo, string descripcion, int tipo, int min, int max, int estado);

        [OperationContract]
        string GuardarMaterial(Material material);

        [OperationContract]
        string ActualizarMaterial(Material material);

        [OperationContract]

        string EliminarMaterial(int id);
    }
}
