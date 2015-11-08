using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Common;
using MetalForming.Data;

namespace MetalForming.BusinessLogic
{
    public class ProgramaBL : BaseLogic
    {
        private readonly ProgramaDA _programaDA = new ProgramaDA();
        private readonly OrdenVentaDA _ordenVentaDA = new OrdenVentaDA();

        public IList<Programa> ListrarPorPlan(int idPlan)
        {
            try
            {
                return _programaDA.ListrarPorPlan(idPlan);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public Programa ObtenerVigente()
        {
            try
            {
                return _programaDA.ObtenerPorEstado(Constantes.EstadoPrograma.Programado);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public string Guardar(Programa programa)
        {
            string numeroPrograma;
            try
            {
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    if (programa.Id == 0)
                    {
                        programa.Estado = Constantes.EstadoPrograma.Programado;

                        _programaDA.Registrar(programa);

                        foreach (var ordenVenta in programa.OrdenesVenta)
                        {
                            _ordenVentaDA.ActualizarPrograma(ordenVenta.Id, programa.Id, Constantes.EstadoOrdenVenta.Programado);
                        }
                    }
                    else
                    {
                        _programaDA.Actualizar(programa);

                        _ordenVentaDA.ActualizarProgramasHaciaNull(programa.Id, Constantes.EstadoOrdenVenta.Pendiente);

                        foreach (var ordenVenta in programa.OrdenesVenta)
                        {
                            _ordenVentaDA.ActualizarPrograma(ordenVenta.Id, programa.Id, Constantes.EstadoOrdenVenta.Programado);
                        }
                    }

                    numeroPrograma = programa.Numero;

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
            return numeroPrograma;
        }

        public void Finalizar(int idPrograma, string motivo)
        {
            try
            {
                var programa = _programaDA.ObtenerPorID(idPrograma);

                programa.Estado = Constantes.EstadoPrograma.Finalizado;
                programa.MotivoFinalizacion = motivo;
                programa.FechaFinalizacion = DateTime.Now;

                _programaDA.Actualizar(programa);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
