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

        public IList<OrdenProduccion> Listar()
        {
            try
            {
                return _ordenProduccionDA.Listar();
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
                return
                    _ordenProduccionDA.Listar()
                        .Where(p => p.Estado == Constantes.EstadoOrdenPoduccion.Programado || 
                                    p.Estado == Constantes.EstadoOrdenPoduccion.ReProgramado)
                        .ToList();
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
                        _productoDA.ActualizarStock(ordenProduccion.OrdenVenta.Producto.Id, -1 * ordenProduccion.CantidadProducto);

                        //Cambiar estado a Orden de Venta
                        _ordenVentaDA.ActualizarEstado(ordenProduccion.OrdenVenta.Id, Constantes.EstadoOrdenVenta.Iniciado);
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
                        }

                        //Registrar Maquinas
                        foreach (var secuencia in ordenProduccion.Secuencia)
                        {
                            secuencia.IdOrdenProduccion = idOrdenProduccion;

                            _ordenProduccionDA.RegistrarSecuencia(secuencia);
                        }

                        //Reserva de producto --> Disminuir el Stock de Producto
                        _productoDA.ActualizarStock(ordenProduccion.OrdenVenta.Producto.Id, -1 * ordenProduccion.CantidadProducto);

                        //Cambiar estado a Orden de Venta
                        _ordenVentaDA.ActualizarEstado(ordenProduccion.OrdenVenta.Id, Constantes.EstadoOrdenVenta.Iniciado);
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
    }
}
