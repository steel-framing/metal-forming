using System.Collections.Generic;
using System.ServiceModel;
using MetalForming.BusinessEntities;
using Microsoft.SqlServer.Server;

namespace MetalForming.ServiceContracts
{
    [ServiceContract]
    public interface IProduccionService
    {
        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVenta();

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPendiente();

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPorPrograma(int idPrograma);

        [OperationContract]
        OrdenVenta ObtenerOrdenVentaPorNumero(string numero);

        [OperationContract]
        IList<Material> ListarMaterialesPorProducto(int idProducto);

        [OperationContract]
        IList<Maquina> ListarMaquinaPorBusqueda(string descripcion, string tipo, string pld, string configuracion);

        [OperationContract]
        Maquina ObtenerMaquinaPorID(int idMaquina);

        [OperationContract]
        int RegistrarOrdenProduccion(OrdenProduccion ordenProduccion);

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccion();

        [OperationContract]
        OrdenProduccion ObetenerOrdenProduccionPorNumero(string numero);

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccionParaEjecucion();

        [OperationContract]
        Plan ObtenerPlanVigente();

        [OperationContract]
        IList<Programa> ListrarProgramasPorPlan(int idPlan);
    }
}
