using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.ProduccionService;
using System.IO;
using System.Text;

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
                        Estado = item.Estado,
                        PLC = item.Maquina.PLD
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
        public JsonResult IniciarOrdenProduccion()
        {
            var response = new JsonResponse();
            try
            {
                foreach (var secuencia in OrdenProduccionActual.Secuencia)
                {
                    var directorio = string.Format(@"C:\MetalForming\{0}\{1}", OrdenProduccionActual.Numero, secuencia.PLC);

                    Directory.CreateDirectory(directorio);

                    var archivo = Path.Combine(directorio, "plc.txt");

                    if (System.IO.File.Exists(archivo))
                        System.IO.File.Delete(archivo);

                    System.IO.File.Create(archivo);
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

        public JsonResult ProcesarOrdenProduccion(int idMaquina)
        {
            var maquinaActual = OrdenProduccionActual.Secuencia.FirstOrDefault(p => p.IdMaquina == idMaquina);

            var archivo = string.Format(@"C:\MetalForming\{0}\{1}\plc.txt", OrdenProduccionActual.Numero, maquinaActual.PLC);

            var texto = string.Empty;

            using (StreamReader reader = new StreamReader(archivo))
            {
                texto = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(texto))
            {
                using (var stream = new FileStream(archivo, FileMode.Open, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("#Maquina:" + maquinaActual.DescripcionMaquina + Environment.NewLine);
                        writer.WriteLine("#FechaInicioProduccion:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + Environment.NewLine);
                        writer.WriteLine("#FechaFinProduccion:" + Environment.NewLine);
                        writer.WriteLine("#Longitud:" + Environment.NewLine);
                        writer.WriteLine("#Espesor:3" + Environment.NewLine);
                        writer.WriteLine("#Ciclo:1-5" + Environment.NewLine);
                        writer.WriteLine("#NoCiclos:" + Environment.NewLine);
                        writer.WriteLine("#MotivosDeParada:" + Environment.NewLine);
                        writer.WriteLine("#TiempoParada:" + Environment.NewLine);
                        writer.WriteLine("#TiempoProduccion:" + Environment.NewLine);
                        writer.WriteLine("#UnidadesProducidas:" + Environment.NewLine);
                        writer.WriteLine("#UnidadesDefectuosas:");
                    }
                }
            }
            else
            {

            }

            return Json("");
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