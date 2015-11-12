using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;
using MetalForming.Common;
using System.Transactions;

namespace MetalForming.BusinessLogic
{
    public class OrdenVentaBL : BaseLogic
    {
        private readonly OrdenVentaDA _orderVentaDA = new OrdenVentaDA();

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

        public IList<OrdenVenta> ListarParaAsignar(int idPrograma)
        {
            try
            {
                return _orderVentaDA.ListarParaAsignar(idPrograma, Constantes.EstadoOrdenVenta.Asignado, Constantes.EstadoOrdenVenta.PendienteAsignar);
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

        public void GuardarAsignaciones(IList<int> ordenesVenta, IList<Usuario> asistentes)
        {
            try
            {
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    foreach (var idOrdenVenta in ordenesVenta)
                    {
                        var ordenVenta = _orderVentaDA.ObtenerPorID(idOrdenVenta);

                        var asistenteConMenorCarga = asistentes.OrderBy(p => p.CantidadOV).FirstOrDefault();

                        if (asistenteConMenorCarga != null)
                        {
                            asistenteConMenorCarga.CantidadOV++;

                            ordenVenta.Estado = Constantes.EstadoOrdenVenta.Asignado;
                            ordenVenta.AsistentePlaneamiento = asistenteConMenorCarga;

                            _orderVentaDA.AsignarAsistentePlaneamiento(ordenVenta);
                        }
                    }

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
