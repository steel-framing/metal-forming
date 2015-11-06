using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MetalForming.Common;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.ProduccionService;

namespace MetalForming.Web.Controllers
{
    public class ProgramacionController : BaseController
    {
        #region Acciones

        #region Mantener Programa

        [HttpGet]
        public ActionResult MantenerPrograma()
        {
            var model = new ProgramaModel();
            try
            {
                using (var service = new ProduccionServiceClient())
                {
                    model.PlanVigente = service.ObtenerPlanVigente();
                }

                using (var service = new ProduccionServiceClient())
                {
                    model.OrdenesVenta = service.ListarOrdenesVentaPendiente();
                }

                if (model.PlanVigente != null && model.PlanVigente.Estado != Constantes.EstadoPlan.Pendiente)
                {
                    using (var service = new ProduccionServiceClient())
                    {
                        model.ProgramasAnteriores = service.ListrarProgramasPorPlan(model.PlanVigente.Id);
                    }

                    if (model.ProgramasAnteriores.Any(p => p.Estado == Constantes.EstadoPrograma.Iniciado))
                    {
                        var programaVigente =
                            model.ProgramasAnteriores.LastOrDefault(
                                p => p.Estado == Constantes.EstadoOrdenPoduccion.Programado);

                        if (programaVigente != null)
                        {
                            model.Id = programaVigente.Id;
                            model.Numero = programaVigente.Numero;
                            model.FechaInicio = programaVigente.FechaInicio;
                            model.FechaFin = programaVigente.FechaFin;
                            model.CantidadOV = programaVigente.CantidadOV;
                            model.Estado = programaVigente.Estado;

                            model.ProgramasAnteriores.Remove(programaVigente);   
                        }

                        IList<OrdenVenta> ordenesVentaPorPrograma;
                        using (var service = new ProduccionServiceClient())
                        {
                             ordenesVentaPorPrograma = service.ListarOrdenesVentaPorPrograma(model.Id);
                        }
                        if (ordenesVentaPorPrograma.Count > 0)
                        {
                            foreach (var ordenVenta in ordenesVentaPorPrograma)
                            {
                                model.OrdenesVenta.Add(ordenVenta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GuardarPrograma(ProgramaModel model)
        {
            var response = new JsonResponse();
            try
            {
                var fechaInicio = Utils.ConvertDate(model.FechaInicioStr, "dd/MM/yyyy");
                var fechaFin = Utils.ConvertDate(model.FechaFinStr, "dd/MM/yyyy");

                var programa = new Programa
                {
                    Id = model.Id,
                    Numero = model.Numero,
                    FechaInicio = fechaInicio.Value,
                    FechaFin = fechaFin.Value,
                    CantidadOV = model.CantidadOV,
                    Estado = model.Estado,
                    IdPlan = model.IdPlan
                };



                response.Data = "";
                response.Success = true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                response.Message = ex.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult FinalizarPrograma(int idPrograma)
        {
            var response = new JsonResponse();
            try
            {

            }
            catch (Exception ex)
            {
                LogError(ex);
                response.Message = ex.Message;
            }
            return Json(response);
        }

        #endregion

        #region Mantener Orden Producción

        [HttpGet]
        public ActionResult MantenerOrdenProduccion()
        {
            var model = new ProgramacionModel();
            try
            {
                IList<OrdenVenta> ordenesVenta;
                using (var service = new ProduccionServiceClient())
                {
                    ordenesVenta = service.ListarOrdenesVenta();
                }

                foreach (var item in ordenesVenta)
                {
                    model.OrdenesVenta.Add(new OrdenVentaModel
                    {
                        Id = item.Id,
                        Numero = item.Numero,
                        Cliente = item.Cliente,
                        FechaEntrega = item.FechaEntrega,
                        Estado = item.Estado
                    });
                }

                IList<OrdenProduccion> ordenesProduccion;
                using (var service = new ProduccionServiceClient())
                {
                    ordenesProduccion = service.ListarOrdenesProduccion();
                }

                foreach (var item in ordenesProduccion)
                {
                    model.OrdenesProduccion.Add(new OrdenProduccionModel
                    {
                        Id = item.Id,
                        Numero = item.Numero,
                        NumeroOrdenVenta = item.OrdenVenta.Numero,
                        FechaEntrega = item.OrdenVenta.FechaEntrega,
                        DescripcionProducto = item.OrdenVenta.Producto.Descripcion,
                        Estado = item.Estado
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
        public ActionResult VerOrdenProduccion(string numero)
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
                model.CantidadOrdenVenta = ordenProduccion.OrdenVenta.Cantidad;
                model.DescripcionProducto = ordenProduccion.OrdenVenta.Producto.Descripcion;
                model.FechaEntrega = ordenProduccion.OrdenVenta.FechaEntrega;
                model.NumeroOrdenVenta = ordenProduccion.Numero;
                model.StockMinimoProducto = ordenProduccion.OrdenVenta.Producto.StockMinimo;
                model.StockProducto = ordenProduccion.OrdenVenta.Producto.Stock;
                model.CantidadProducto = ordenProduccion.OrdenVenta.Cantidad + ordenProduccion.OrdenVenta.Producto.StockMinimo - ordenProduccion.OrdenVenta.Producto.Stock;

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
                        FechaFin = item.FechaFin
                    });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View("OrdenProduccion", model);
        }

        [HttpGet]
        public ActionResult CrearOrdenProduccion(string numero)
        {
            var model = new OrdenProduccionModel();
            try
            {
                OrdenVenta ordenVenta;
                using (var service = new ProduccionServiceClient())
                {
                    ordenVenta = service.ObtenerOrdenVentaPorNumero(numero);
                }

                if (ordenVenta == null)
                {
                    throw new Exception("El número (" + numero + ") de Orden de Venta no existe.");
                }

                model.IdOrdenVenta = ordenVenta.Id;
                model.CantidadOrdenVenta = ordenVenta.Cantidad;
                model.IdProducto = ordenVenta.Producto.Id;
                model.DescripcionProducto = ordenVenta.Producto.Descripcion;
                model.FechaEntrega = ordenVenta.FechaEntrega;
                model.NumeroOrdenVenta = ordenVenta.Numero;
                model.StockMinimoProducto = ordenVenta.Producto.StockMinimo;
                model.StockProducto = ordenVenta.Producto.Stock;
                model.CantidadProducto = model.CantidadOrdenVenta + model.StockMinimoProducto - model.StockProducto;

                List<Material> materiales = null;
                using (var service = new ProduccionServiceClient())
                {
                    materiales = service.ListarMaterialesPorProducto(ordenVenta.Producto.Id);
                }

                foreach (var material in materiales)
                {
                    var materialRequerido = new OrdenProduccionMaterialModel
                    {
                        DescripcionMaterial = material.Descripcion,
                        IdMaterial = material.Id,
                        Stock = material.Stock,
                        StockMinimo = material.StockMinimo,
                        Requerido = material.Cantidad, //Cantidad necesaria para un Producto
                        Reservado = material.Reservado
                    };

                    model.Materiales.Add(materialRequerido);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View("OrdenProduccion", model);
        }

        [HttpPost]
        public JsonResult BuscarMaquina(string descripcion, string tipo, string pld, string configuracion)
        {
            var response = new JsonResponse();
            try
            {
                IList<Maquina> maquinas;
                using (var service = new ProduccionServiceClient())
                {
                    maquinas = service.ListarMaquinaPorBusqueda(descripcion, tipo, pld, configuracion);
                }

                response.Data = maquinas;
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

        [HttpPost]
        public JsonResult ObtenerMaquina(int idMaquina)
        {
            var response = new JsonResponse();
            try
            {
                Maquina maquina;
                using (var service = new ProduccionServiceClient())
                {
                    maquina = service.ObtenerMaquinaPorID(idMaquina);
                }

                response.Data = maquina;
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

        [HttpPost]
        public JsonResult GuardarOrdenProduccion(OrdenProduccionModel model)
        {
            var response = new JsonResponse();
            try
            {
                var ordenProduccion = new OrdenProduccion
                {
                    CantidadProducto = model.CantidadProducto,
                    TomarStock = model.TomarStock,
                    Estado = Constantes.EstadoOrdenPoduccion.Programado,
                    OrdenVenta = new OrdenVenta
                    {
                        Id = model.IdOrdenVenta,
                        Producto = new Producto { Id = model.IdProducto }
                    },
                    Materiales = new List<OrdenProduccionMaterial>(),
                    Secuencia = new List<OrdenProduccionSecuencia>()
                };

                foreach (var material in model.Materiales)
                {
                    ordenProduccion.Materiales.Add(new OrdenProduccionMaterial
                    {
                        Comprar = material.Comprar,
                        Requerido = material.Requerido,
                        Material = new Material { Id = material.IdMaterial }
                    });
                }

                foreach (var secuencia in model.Secuencia)
                {
                    var fechaInicio = Utils.ConvertDate(secuencia.FechaInicioStr, "dd/MM/yyyy HH:mm");
                    var fechaFin = Utils.ConvertDate(secuencia.FechaFinStr, "dd/MM/yyyy HH:mm");

                    ordenProduccion.Secuencia.Add(new OrdenProduccionSecuencia
                    {
                        Secuencia = secuencia.Secuencia,
                        Maquina = new Maquina { Id = secuencia.IdMaquina },
                        FechaInicio = fechaInicio.Value,
                        FechaFin = fechaFin.Value
                    });
                }

                var idOrdenProduccion = 0;
                using (var service = new ProduccionServiceClient())
                {
                    idOrdenProduccion = service.RegistrarOrdenProduccion(ordenProduccion);
                }

                response.Data = idOrdenProduccion;
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