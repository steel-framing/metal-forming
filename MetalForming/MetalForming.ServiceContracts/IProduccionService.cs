using System.Collections.Generic;
using System.ServiceModel;
using MetalForming.BusinessEntities;

namespace MetalForming.ServiceContracts
{
    [ServiceContract]
    public interface IProduccionService
    {
        #region Orden Venta

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVenta();

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPendiente();

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPorPrograma(int idPrograma);

        [OperationContract]
        OrdenVenta ObtenerOrdenVentaPorNumero(string numero);

        #endregion

        #region Material

        [OperationContract]
        IList<Material> ListarMaterialesPorProducto(int idProducto);

        #endregion

        #region Maquina

        [OperationContract]
        IList<Maquina> ListarMaquinaPorBusqueda(string descripcion, string tipo, string pld, string configuracion);

        [OperationContract]
        Maquina ObtenerMaquinaPorID(int idMaquina);

        #endregion

        #region Orden Produccion

        [OperationContract]
        int RegistrarOrdenProduccion(OrdenProduccion ordenProduccion);

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccion();

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccionPorPrograma(int idPrograma);

        [OperationContract]
        OrdenProduccion ObetenerOrdenProduccionPorNumero(string numero);

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccionParaEjecucion();

        [OperationContract]
        void AprobarMasivoOrdenesProduccion(IList<int> ordenesProduccion);

        [OperationContract]
        void AprobarIndividualOrdenProduccion(int idOrdenProduccion);

        [OperationContract]
        void RechazarOrdenProduccion(int idOrdenProduccion, string motivoRechazo);

        #endregion

        #region Plan

        [OperationContract]
        Plan ObtenerPlanVigente();

        #endregion

        #region Programa

        [OperationContract]
        IList<Programa> ListrarProgramasPorPlan(int idPlan);

        [OperationContract]
        Programa ObtenerProgramaVigente();

        [OperationContract]
        string GuardarPrograma(Programa programa);

        [OperationContract]
        void FinalizarPrograma(int idPrograma, string motivo);

        #endregion
    }
}
