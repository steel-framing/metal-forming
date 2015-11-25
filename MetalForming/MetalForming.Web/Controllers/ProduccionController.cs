using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.ProduccionService;
using System.IO;
using System.Text;
using MetalForming.Common;
using System.Threading;
using System.Reflection;

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
                model.CantidadProducto = ordenProduccion.CantidadProducto;

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
                        PLC = item.Maquina.PLD,
                        Ciclo = item.Maquina.Ciclo,
                        Longitud = item.Maquina.Longitud,
                        Espesor = item.Maquina.Espesor
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
        public JsonResult CrearCarpetasArcivosPLC()
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

                    var file = System.IO.File.Create(archivo);
                    file.Close();
                    file.Dispose();
                }

                using (var service = new ProduccionServiceClient())
                {
                    service.ActualizarEstadoOrdenProduccion(OrdenProduccionActual.Id, Constantes.EstadoOrdenPoduccion.Conformado);
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

        [HttpPost]
        public JsonResult IniciarProceso(int idMaquina)
        {
            var response = new JsonResponse();
            try
            {
                var maquinaActual = OrdenProduccionActual.Secuencia.FirstOrDefault(p => p.IdMaquina == idMaquina);

                var archivo = string.Format(@"C:\MetalForming\{0}\{1}\plc.txt", OrdenProduccionActual.Numero, maquinaActual.PLC);

                using (var stream = new FileStream(archivo, FileMode.Open, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("#Maquina:" + maquinaActual.DescripcionMaquina);
                        writer.WriteLine("#FechaInicioProduccion:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        writer.WriteLine("#FechaFinProduccion:");
                        writer.WriteLine("#Longitud:" + maquinaActual.Longitud);
                        writer.WriteLine("#Espesor:" + maquinaActual.Espesor);
                        writer.WriteLine("#Ciclo:" + maquinaActual.Ciclo);
                        writer.WriteLine("#NoCiclos:");
                        writer.WriteLine("#MotivosDeParada:");
                        writer.WriteLine("#TiempoParada:");
                        writer.WriteLine("#TiempoProduccion:");
                        writer.WriteLine("#UnidadesAProducir:" + OrdenProduccionActual.CantidadProducto);
                        writer.WriteLine("#UnidadesProducidas:");
                        writer.WriteLine("#UnidadesDefectuosas:");
                    }
                }

                using (var service = new ProduccionServiceClient())
                {
                    service.ActualizarEstadoOrdenProduccionSecuencia(OrdenProduccionActual.Id, idMaquina, Constantes.EstadoProcesoMaquina.EnProceso);
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

        [HttpPost]
        public JsonResult LeerArchivo(int idMaquina)
        {
            var response = new JsonResponse();
            try
            {
                var maquinaActual = OrdenProduccionActual.Secuencia.FirstOrDefault(p => p.IdMaquina == idMaquina);

                var archivo = string.Format(@"C:\MetalForming\{0}\{1}\plc.txt", OrdenProduccionActual.Numero, maquinaActual.PLC);

                var texto = System.IO.File.ReadAllText(archivo);
                var lineas = texto.Split('\n');

                var model = new DatosArchivoModel
                {
                    Maquina = lineas[0].Replace("#Maquina:", "").Replace("\r", ""),
                    FechaInicioProduccion = lineas[1].Replace("#FechaInicioProduccion:", "").Replace("\r", ""),
                    FechaFinProduccion = lineas[2].Replace("#FechaFinProduccion:", "").Replace("\r", ""),
                    Longitud = lineas[3].Replace("#Longitud:", "").Replace("\r", ""),
                    Espesor = lineas[4].Replace("#Espesor:", "").Replace("\r", ""),
                    Ciclo = lineas[5].Replace("#Ciclo:", "").Replace("\r", ""),
                    NoCiclos = lineas[6].Replace("#NoCiclos:", "").Replace("\r", ""),
                    MotivosDeParada = lineas[7].Replace("#MotivosDeParada:", "").Replace("\r", ""),
                    TiempoParada = lineas[8].Replace("#TiempoParada:", "").Replace("\r", ""),
                    TiempoProduccion = lineas[9].Replace("#TiempoProduccion:", "").Replace("\r", ""),
                    UnidadesProducidas = lineas[10].Replace("#UnidadesProducidas:", "").Replace("\r", ""),
                    UnidadesAProducidas = lineas[11].Replace("#UnidadesAProducir:", "").Replace("\r", ""),
                    UnidadesDefectuosas = lineas[12].Replace("#UnidadesDefectuosas:", "").Replace("\r", ""),
                    Paradas = new List<ParadaModel>()
                };

                model.MotivosDeParada = string.IsNullOrEmpty(model.MotivosDeParada) ? "" : model.MotivosDeParada;
                model.TiempoParada = string.IsNullOrEmpty(model.TiempoParada) ? "" : model.TiempoParada;

                var motivos = model.MotivosDeParada.Split(',').Where(p => !string.IsNullOrEmpty(p)).ToList();
                var tiempos = model.TiempoParada.Split(',').Where(p => !string.IsNullOrEmpty(p)).ToList();

                for (int i = 0; i < motivos.Count; i++)
                {
                    //Obtener el valor de la constante de manera dinamica
                    var constante = typeof(Constantes.MotivosDeParada).GetFields().First(f => f.Name.Equals("Motivo" + motivos.ElementAtOrDefault(i)));
                    if (constante == null)
                    {
                        model.Paradas.Add(new ParadaModel
                        {
                            Motivo = motivos.ElementAtOrDefault(i),
                            Mensaje = string.Empty,
                            Tiempo = tiempos.ElementAtOrDefault(i)
                        });
                    }
                    else
                    {
                        model.Paradas.Add(new ParadaModel
                        {
                            Motivo = motivos.ElementAtOrDefault(i),
                            Mensaje = constante.GetRawConstantValue().ToString(),
                            Tiempo = tiempos.ElementAtOrDefault(i)
                        });
                    }
                }

                response.Data = model;
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
        public JsonResult ActualizarEstado(string estado)
        {
            var response = new JsonResponse();
            try
            {
                using (var service = new ProduccionServiceClient())
                {
                    service.ActualizarEstadoOrdenProduccion(OrdenProduccionActual.Id, estado);
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

        [HttpPost]
        public JsonResult ActualizarEstadoSecuencia(int idMaquina, string estado)
        {
            var response = new JsonResponse();
            try
            {
                using (var service = new ProduccionServiceClient())
                {
                    service.ActualizarEstadoOrdenProduccionSecuencia(OrdenProduccionActual.Id, idMaquina, estado);
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

        [HttpPost]
        public JsonResult GenerarArchivoPrueba(int idMaquina, int numero)
        {
            var response = new JsonResponse();
            try
            {
                var maquinaActual = OrdenProduccionActual.Secuencia.FirstOrDefault(p => p.IdMaquina == idMaquina);

                var archivo = string.Format(@"C:\MetalForming\{0}\{1}\plc.txt", OrdenProduccionActual.Numero, maquinaActual.PLC);

                switch (numero)
                {
                    case 1: //Generar siguiente ciclo
                        {
                            var texto = System.IO.File.ReadAllText(archivo);
                            var lineas = texto.Split('\n');

                            var fechaInicioProduccion = lineas[1].Replace("#FechaInicioProduccion:", "").Replace("\r", "");

                            var noCiclos = lineas[6].Replace("#NoCiclos:", "").Replace("\r", "");
                            if (string.IsNullOrEmpty(noCiclos))
                                noCiclos = "1";
                            else
                                noCiclos = (Convert.ToInt32(noCiclos) + 1).ToString();

                            var builder = new StringBuilder();
                            builder.AppendLine("#Maquina:" + maquinaActual.DescripcionMaquina);
                            builder.AppendLine("#FechaInicioProduccion:" + fechaInicioProduccion);
                            builder.AppendLine("#FechaFinProduccion:");
                            builder.AppendLine("#Longitud:" + maquinaActual.Longitud);
                            builder.AppendLine("#Espesor:" + maquinaActual.Espesor);
                            builder.AppendLine("#Ciclo:" + maquinaActual.Ciclo);
                            builder.AppendLine("#NoCiclos:" + noCiclos);
                            builder.AppendLine("#MotivosDeParada:");
                            builder.AppendLine("#TiempoParada:");
                            builder.AppendLine("#TiempoProduccion:");
                            builder.AppendLine("#UnidadesAProducir:" + OrdenProduccionActual.CantidadProducto);
                            builder.AppendLine("#UnidadesProducidas:");
                            builder.AppendLine("#UnidadesDefectuosas:");

                            System.IO.File.WriteAllText(archivo, builder.ToString());
                        }
                        break;
                    case 2: //Generar error
                        {
                            var texto = System.IO.File.ReadAllText(archivo);
                            var lineas = texto.Split('\n');

                            var fechaInicioProduccion = lineas[1].Replace("#FechaInicioProduccion:", "").Replace("\r", "");

                            var noCiclos = lineas[6].Replace("#NoCiclos:", "").Replace("\r", "");
                            if (string.IsNullOrEmpty(noCiclos))
                                noCiclos = "1";
                            else
                                noCiclos = (Convert.ToInt32(noCiclos) + 1).ToString();

                            var builder = new StringBuilder();
                            builder.AppendLine("#Maquina:" + maquinaActual.DescripcionMaquina);
                            builder.AppendLine("#FechaInicioProduccion:" + fechaInicioProduccion);
                            builder.AppendLine("#FechaFinProduccion:");
                            builder.AppendLine("#Longitud:" + maquinaActual.Longitud);
                            builder.AppendLine("#Espesor:" + maquinaActual.Espesor);
                            builder.AppendLine("#Ciclo:" + maquinaActual.Ciclo);
                            builder.AppendLine("#NoCiclos:" + noCiclos);
                            builder.AppendLine("#MotivosDeParada:1");
                            builder.AppendLine("#TiempoParada:");
                            builder.AppendLine("#TiempoProduccion:");
                            builder.AppendLine("#UnidadesAProducir:" + OrdenProduccionActual.CantidadProducto);
                            builder.AppendLine("#UnidadesProducidas:");
                            builder.AppendLine("#UnidadesDefectuosas:");

                            System.IO.File.WriteAllText(archivo, builder.ToString());
                        }
                        break;
                    case 3: //Generar error corregido
                        {

                        }
                        break;
                    case 4: //Generar termino
                        {

                        }
                        break;
                    default:
                        break;
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