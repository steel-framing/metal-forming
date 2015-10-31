using MetalForming.BusinessEntities;
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

        public IList<OrdenVenta> ListarOrdenesVenta()
        {
            return _ordenVentaBL.Listar();
        }

        public OrdenVenta ObtenerOrdenVentaPorNumero(string numero)
        {
            return _ordenVentaBL.ObtenerPorNumero(numero);
        }

        public IList<Material> ListarMaterialesPorProducto(int idProducto)
        {
            return _materialBL.ListarPorProducto(idProducto);
        }

        public IList<Maquina> ListarMaquinaPorBusqueda(string descripcion, string tipo, string pld, string configuracion)
        {
            return _maquinaBL.ListarPorBusqueda(descripcion, tipo, pld, configuracion);
        }

        public Maquina ObtenerMaquinaPorID(int idMaquina)
        {
            return _maquinaBL.ObtenerPorID(idMaquina);
        }

        public int RegistrarOrdenProduccion(OrdenProduccion ordenProduccion)
        {
            return _ordenProduccionBL.Registrar(ordenProduccion);
        }

        public IList<OrdenProduccion> ListarOrdenesProduccion()
        {
            return _ordenProduccionBL.Listar();
        }

        public OrdenProduccion ObetenerOrdenProduccionPorNumero(string numero)
        {
            return _ordenProduccionBL.ObetenerPorNumero(numero);
        }
    }
}
