﻿using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic;
using MetalForming.ServiceContracts;
using System.Collections.Generic;
using System.ServiceModel.Activation;

namespace MetalForming.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProduccionService : IProduccionService
    {
        private readonly OrdenVentaBL _ordenVentaBL = new OrdenVentaBL();
        private readonly MaterialBL _materialBL = new MaterialBL();
        private readonly MaquinaBL _maquinaBL = new MaquinaBL();
        private readonly OrdenProduccionBL _ordenProduccionBL = new OrdenProduccionBL();
        private readonly PlanBL _planBL = new PlanBL();
        private readonly ProgramaBL _programaBL = new ProgramaBL();

        #region Orden Venta

        //depurar
        public IList<OrdenVenta> ListarOrdenesVenta()
        {
            return _ordenVentaBL.Listar();
        }

        public IList<OrdenVenta> ListarOrdenesVentaPendiente()
        {
            return _ordenVentaBL.ListarPendientes();
        }

        public IList<OrdenVenta> ListarOrdenesVentaPorPrograma(int idPrograma)
        {
            return _ordenVentaBL.ListarPorPrograma(idPrograma);
        }

        public OrdenVenta ObtenerOrdenVentaPorNumero(string numero)
        {
            return _ordenVentaBL.ObtenerPorNumero(numero);
        }

        #endregion

        #region Material

        public IList<Material> ListarMaterialesPorProducto(int idProducto)
        {
            return _materialBL.ListarPorProducto(idProducto);
        }

        #endregion

        #region Maquina

        public IList<Maquina> ListarMaquinaPorBusqueda(string descripcion, string tipo, string pld, string configuracion)
        {
            return _maquinaBL.ListarPorBusqueda(descripcion, tipo, pld, configuracion);
        }

        public Maquina ObtenerMaquinaPorID(int idMaquina)
        {
            return _maquinaBL.ObtenerPorID(idMaquina);
        }

        #endregion

        #region Orden Produccion

        public int RegistrarOrdenProduccion(OrdenProduccion ordenProduccion)
        {
            return _ordenProduccionBL.Registrar(ordenProduccion);
        }

        //depurar
        public IList<OrdenProduccion> ListarOrdenesProduccion()
        {
            return _ordenProduccionBL.Listar();
        }

        public IList<OrdenProduccion> ListarOrdenesProduccionPorPrograma(int idPrograma)
        {
            return _ordenProduccionBL.ListarPorPrograma(idPrograma);
        }

        public OrdenProduccion ObetenerOrdenProduccionPorNumero(string numero)
        {
            return _ordenProduccionBL.ObetenerPorNumero(numero);
        }

        public IList<OrdenProduccion> ListarOrdenesProduccionParaEjecucion()
        {
            return _ordenProduccionBL.ListarParaEjecucion();
        }

        #endregion

        #region Plan

        public Plan ObtenerPlanVigente()
        {
            return _planBL.ObtenerVigente();
        }

        #endregion

        #region Programa

        public IList<Programa> ListrarProgramasPorPlan(int idPlan)
        {
            return _programaBL.ListrarPorPlan(idPlan);
        }

        public Programa ObtenerProgramaVigente()
        {
            return _programaBL.ObtenerVigente();
        }

        public string GuardarPrograma(Programa programa)
        {
            return _programaBL.Guardar(programa);
        }

        public void FinalizarPrograma(int idPrograma, string motivo)
        {
            _programaBL.Finalizar(idPrograma, motivo);
        }

        #endregion
    }
}
