using System.Collections.Generic;
using System.ServiceModel;
using MetalForming.BusinessEntities;
using System;

namespace MetalForming.ServiceContracts
{
    [ServiceContract]
    public interface IProduccionService
    {
        #region Orden Venta

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPendiente();

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaParaAsignar(int idPrograma);

        [OperationContract]
        IList<OrdenVenta> ListarOrdenesVentaPorPrograma(int idPrograma);

        [OperationContract]
        OrdenVenta ObtenerOrdenVentaPorNumero(string numero);

        [OperationContract]
        void GuardarAsignacionesOrdeneVenta(IList<int> ordenesVenta, IList<Usuario> asistentes);

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
        IList<OrdenProduccion> ListarOrdenesProduccionPorPrograma(int idPrograma);

        [OperationContract]
        IList<OrdenProduccion> ListarOrdenesProduccionParaAsignar(int idPrograma);

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

        [OperationContract]
        void GuardarAsignacionesOrdenProduccion(IList<int> ordenesProduccion, IList<Usuario> operadores);

        [OperationContract]
        void ActualizarEstadoOrdenProduccion(int id, string estado);

        [OperationContract]
        void ActualizarEstadoOrdenProduccionSecuencia(int id, int idMaquina, string estado);

        [OperationContract]
        int ValidarHorarioOrdenProduccionSecuencia(int idMaquina, DateTime fechaInicio, DateTime fechaFin);

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

        #region Usuario

        [OperationContract]
        IList<Usuario> ListarAsistentePlaneamiento();

        [OperationContract]
        IList<Usuario> ListarOperadores();

        #endregion
    }
}
