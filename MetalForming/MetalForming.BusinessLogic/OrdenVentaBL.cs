using System;
using System.Collections.Generic;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;
using MetalForming.Common;

namespace MetalForming.BusinessLogic
{
    public class OrdenVentaBL : BaseLogic
    {
        private readonly OrdenVentaDA _orderVentaDA = new OrdenVentaDA();

        public IList<OrdenVenta> Listar()
        {
            try
            {
                return _orderVentaDA.Listar();
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public IList<OrdenVenta> ListarPendientes()
        {
            try
            {
                return _orderVentaDA.ListarPendientePrograma();
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public IList<OrdenVenta> ListarPorPrograma(int idPrograma)
        {
            try
            {
                return _orderVentaDA.ListarPorPrograma(idPrograma);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public OrdenVenta ObtenerPorNumero(string numero)
        {
            try
            {
                return _orderVentaDA.ObtenerPorNumero(numero);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name, numero);
            }
        }
    }
}
