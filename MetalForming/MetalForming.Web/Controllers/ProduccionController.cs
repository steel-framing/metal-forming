using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.ProduccionService;

namespace MetalForming.Web.Controllers
{
    public class ProduccionController : BaseController
    {
        #region Propiedades

        private OrdenProduccionModel OrdenProduccionActual
        {
            get { return Session["OrdenProduccion"] as OrdenProduccionModel; }
            set { Session.Add("OrdenProduccion", value); }
        }

        #endregion

        #region Acciones

        #region Ejecutar Orden Producción

        [HttpGet]
        public ActionResult EjecutarOrdenProduccion()
        {
            var model = new List<OrdenProduccionModel>();
            try
            {
                IList<OrdenProduccion> ordenesProduccion;
                using (var service = new ProduccionServiceClient())
                {
                    ordenesProduccion = service.ListarOrdenesProduccionParaEjecucion();
                }

                foreach (var item in ordenesProduccion)
                {
                    model.Add(new OrdenProduccionModel
                    {
                        Id = item.Id,
                        Numero = item.Numero,
                        NumeroOrdenVenta = item.OrdenVenta.Numero,
                        FechaEntrega = item.OrdenVenta.FechaEntrega,
                        Estado = item.Estado,
                        NombreOperador = item.Operador == null ? string.Empty : item.Operador.NombreCompleto
                    });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EjecucionOrdenProduccion(string numero)
        {
            var model = new OrdenProduccionModel();
            try
            {
                OrdenProduccion ordenProduccion;
                using (var service = new ProduccionServiceClient())
                {
                    ordenProduccion = service.ObetenerOrdenProduccionPorNumero(numero);
                }

                if (ordenProduccion == null)
                {
                    throw new Exception("El número (" + numero + ") de Orden de Producción no existe.");
                }

                model.Id = ordenProduccion.Id;
                model.Numero = ordenProduccion.Numero;
                model.FechaEntrega = ordenProduccion.OrdenVenta.FechaEntrega;
                model.NumeroOrdenVenta = ordenProduccion.Numero;
                model.Estado = ordenProduccion.Estado;

                foreach (var item in ordenProduccion.Materiales)
                {
                    model.Materiales.Add(new OrdenProduccionMaterialModel
                    {
                        IdMaterial = item.Material.Id,
                        DescripcionMaterial = item.Material.Descripcion,
                        Stock = item.Material.Stock,
                        StockMinimo = item.Material.StockMinimo,
                        Requerido = item.Requerido,
                        Reservado = item.Material.Reservado,
                        Comprar = item.Comprar
                    });
                }

                foreach (var item in ordenProduccion.Secuencia)
                {
                    model.Secuencia.Add(new OrdenProduccionSecuenciaModel
                    {
                        IdMaquina = item.Maquina.Id,
                        Secuencia = item.Secuencia,
                        DescripcionMaquina = item.Maquina.Descripcion,
                        PorcentajeFalla = item.Maquina.PorcentajeFalla,
                        Tiempo = item.Maquina.Tiempo,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Estado = item.Estado
                    });
                }

                OrdenProduccionActual = model;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult IniciarOrdenProduccion(string numero)
        {
            var response = new JsonResponse();
            try
            {
                foreach (var secuencia in OrdenProduccionActual.Secuencia)
                {
                       
                }

                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

                LogError(ex);
            }
            return Json(response);
        }

        [HttpGet]
        public ActionResult FinalizarOrdenProduccion(string numero, string tiempo)
        {
            //Cambiar estado -> Producido
            return RedirectToAction("EjecutarOrdenProduccion");
        }

        #endregion

        #region Asignar Orden de Producción

        [HttpGet]
        public ActionResult AsignarOrdenProduccion()
        {
            var model = new AsignarOrdenProduccionModel();
            try
            {
                Programa programaVigente;
                using (var service = new ProduccionServiceClient())
                {
                    programaVigente = service.ObtenerProgramaVigente();
                }

                if (programaVigente != null)
                {
                    model.ProgramaVigente = new ProgramaModel
                    {
                        Id = programaVigente.Id,
                        Numero = programaVigente.Numero,
                        FechaInicio = programaVigente.FechaInicio,
                        FechaFin = programaVigente.FechaFin
                    };

                    IList<OrdenProduccion> ordenesProduccion;
                    using (var service = new ProduccionServiceClient())
                    {
                        ordenesProduccion = service.ListarOrdenesProduccionParaAsignar(model.ProgramaVigente.Id);
                    }

                    foreach (var item in ordenesProduccion)
                    {
                        model.OrdenesProduccion.Add(new OrdenProduccionModel
                        {
                            Id = item.Id,
                            Numero = item.Numero,
                            NumeroOrdenVenta = item.OrdenVenta.Numero,
                            FechaEntrega = item.OrdenVenta.FechaEntrega,
                            Estado = item.Estado,
                            DescripcionProducto = item.OrdenVenta.Producto.Descripcion,
                            NombreOperador = item.Operador == null
                                        ? string.Empty
                                        : item.Operador.NombreCompleto
                        });
                    }
                }

                IList<Usuario> operadores;
                using (var service = new ProduccionServiceClient())
                {
                    operadores = service.ListarOperadores();
                }

                foreach (var operador in operadores)
                {
                    model.Operadores.Add(new UsuarioModel
                    {
                        Id = operador.Id,
                        NombreCompleto = operador.NombreCompleto,
                        Username = operador.Username,
                        CantidadOP = operador.CantidadOP
                    });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GuardarAsignaciones(AsignarOrdenProduccionModel model)
        {
            var response = new JsonResponse();
            try
            {
                using (var service = new ProduccionServiceClient())
                {
                    service.GuardarAsignacionesOrdenProduccion(
                        model.OrdenesProduccion.Select(p => p.Id).ToList(),
                        model.Operadores.Select(p => new Usuario
                        {
                            Id = p.Id,
                            CantidadOP = p.CantidadOP

                        }).ToList());
                }

                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

                LogError(ex);
            }
            return Json(response);
        }

        #endregion

        #endregion
    }
}