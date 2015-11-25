using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Common;
using MetalForming.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace MetalForming.BusinessLogic
{
    public class OrdenProduccionBL : BaseLogic
    {
        private readonly OrdenProduccionDA _ordenProduccionDA = new OrdenProduccionDA();
        private readonly OrdenVentaDA _ordenVentaDA = new OrdenVentaDA();
        private readonly ProductoDA _productoDA = new ProductoDA();
        private readonly MaterialDA _materialDA = new MaterialDA();
        
        public IList<OrdenProduccion> ListarPorPrograma(int idPrograma)
        {
            try
            {
                return _ordenProduccionDA.ListarPorPrograma(idPrograma);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public IList<OrdenProduccion> ListarParaAsignar(int idPrograma)
        {
            try
            {
                return _ordenProduccionDA.ListarParaAsignar(idPrograma, Constantes.EstadoOrdenPoduccion.Programado, Constantes.EstadoOrdenPoduccion.ReProgramado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public IList<OrdenProduccion> ListarParaEjecucion()
        {
            try
            {
                return _ordenProduccionDA.ListarParaEjecutar(Constantes.EstadoOrdenPoduccion.Asignado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public OrdenProduccion ObetenerPorNumero(string numero)
        {
            try
            {
                var ordenProduccion = _ordenProduccionDA.ObetenerPorNumero(numero);

                ordenProduccion.Materiales = _ordenProduccionDA.ListarMaterial(ordenProduccion.Id);
                ordenProduccion.Secuencia = _ordenProduccionDA.ListarSecuencia(ordenProduccion.Id);

                return ordenProduccion;
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name, numero);
            }
        }

        public int Registrar(OrdenProduccion ordenProduccion)
        {
            try
            {
                var idOrdenProduccion = 0;

                var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted };

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    if (ordenProduccion.TomarStock)
                    {
                        //Reserva de producto --> Disminuir el Stock de Producto
                        _productoDA.ActualizarStock(ordenProduccion.OrdenVenta.Producto.Id, -1 * ordenProduccion.OrdenVenta.Cantidad);

                        //Cambiar estado a Orden de Venta
                        _ordenVentaDA.ActualizarEstado(ordenProduccion.OrdenVenta.Id, Constantes.EstadoOrdenVenta.ReservadoStock);
                    }
                    else
                    {
                        //Generar Orden de Producción
                        idOrdenProduccion = _ordenProduccionDA.Registrar(ordenProduccion);

                        //Registrar Materiales
                        foreach (var material in ordenProduccion.Materiales)
                        {
                            material.IdOrdenProduccion = idOrdenProduccion;

                            _ordenProduccionDA.RegistrarMaterial(material);

                            var cantidadUtilizada = material.Comprar - material.Requerido;

                            _materialDA.ActualizarStock(material.Material.Id, cantidadUtilizada);
                        }

                        //Registrar Maquinas
                        foreach (var secuencia in ordenProduccion.Secuencia)
                        {
                            secuencia.IdOrdenProduccion = idOrdenProduccion;
                            secuencia.Estado = Constantes.EstadoProcesoMaquina.Pendiente;

                            _ordenProduccionDA.RegistrarSecuencia(secuencia);
                        }

                        //Reserva de producto --> Stock de Producto
                        _productoDA.ActualizarStock(ordenProduccion.OrdenVenta.Producto.Id, ordenProduccion.CantidadProducto - ordenProduccion.OrdenVenta.Cantidad);

                        //Cambiar estado a Orden de Venta
                        _ordenVentaDA.ActualizarEstado(ordenProduccion.OrdenVenta.Id, Constantes.EstadoOrdenVenta.Programado);
                    }

                    transactionScope.Complete();
                }
                return idOrdenProduccion;
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public void AprobarMasivo(IList<int> ordenesProduccion)
        {
            try
            {
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    foreach (var id in ordenesProduccion)
                    {
                        _ordenProduccionDA.ActualizarEstado(id, Constantes.EstadoOrdenPoduccion.Programado);
                    }

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public void AprobarIndividual(int id)
        {
            try
            {
                _ordenProduccionDA.ActualizarEstado(id, Constantes.EstadoOrdenPoduccion.Programado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public void Rechazar(int id, string motivo)
        {
            try
            {
                _ordenProduccionDA.Rechazar(id, Constantes.EstadoOrdenPoduccion.Rechazado, motivo);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public void GuardarAsignaciones(IList<int> ordenesProduccion, IList<Usuario> operadores)
        {
            try
            {
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    foreach (var idOrdenProduccion in ordenesProduccion)
                    {
                        var ordenProduccion = _ordenProduccionDA.ObetenerPorID(idOrdenProduccion);

                        var operadorConMenorCarga = operadores.OrderBy(p => p.CantidadOV).FirstOrDefault();

                        if (operadorConMenorCarga != null)
                        {
                            operadorConMenorCarga.CantidadOP++;

                            ordenProduccion.Estado = Constantes.EstadoOrdenPoduccion.Asignado;
                            ordenProduccion.Operador = operadorConMenorCarga;

                            _ordenProduccionDA.AsignarAsistentePlaneamiento(ordenProduccion);
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

        public void ActualizarEstado(int id, string estado)
        {
            try
            {
                _ordenProduccionDA.ActualizarEstado(id, estado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public void ActualizarEstadoSecuencia(int id, int idMaquina, string estado)
        {
            try
            {
                _ordenProduccionDA.ActualizarEstadoSecuencia(id, idMaquina, estado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
