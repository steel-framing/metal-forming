using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.ProduccionService;

namespace MetalForming.Web.Controllers
{
    public class ProduccionController : BaseController
    {
        #region Acciones

        #region Ejecutar Orden Producción

        [HttpGet]
        public ActionResult Index()
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
        public ActionResult Ejecucion(string numero)
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
                        FechaFin = item.FechaFin
                    });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return View(model);
        }

        public JsonResult Iniciar(string numero)
        {
            //Cambiar estado -> En Proceso
            return Json("Ok");
        }

        public ActionResult Finalizar(string numero, string tiempo)
        {
            //Cambiar estado -> Producido
            return RedirectToAction("Index");
        }

        #endregion

        #region Asignar Orden de Producción



        #endregion

        #endregion
    }
}