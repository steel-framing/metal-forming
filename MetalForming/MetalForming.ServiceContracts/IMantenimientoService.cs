using System.Collections.Generic;
using System.ServiceModel;
using MetalForming.BusinessEntities;

namespace MetalForming.ServiceContracts
{
    [ServiceContract]
    public interface IMantenimientoService
    {
        [OperationContract]
        IList<Material> ListarMateriales();

    }
}
