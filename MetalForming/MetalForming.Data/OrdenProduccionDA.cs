using MetalForming.BusinessEntities;
using MetalForming.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace MetalForming.Data
{
    public class OrdenProduccionDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorPrograma = "dbo.ListarOrdenProduccionPorPrograma";
        private const string ProcedimientoAlmacenadoListarParaAsginar = "dbo.ListarOrdenProduccionParaAsignar";
        private const string ProcedimientoAlmacenadoListarParaEjecutar = "dbo.ListarOrdenProduccionParaEjecutar";
        private const string ProcedimientoAlmacenadoObtenerPorID = "dbo.ObtenerOrdenProduccionPorID";
        private const string ProcedimientoAlmacenadoObtenerPorNumero = "dbo.ObtenerOrdenProduccionPorNumero";
        private const string ProcedimientoAlmacenadoListarMaterial = "dbo.ListarOrdenProduccionMaterial";
        private const string ProcedimientoAlmacenadoListarSecuencia = "dbo.ListarOrdenProduccionSecuencia";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccion = "dbo.InsertarOrdenProduccion";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccionMaterial = "dbo.InsertarOrdenProduccionMaterial";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccionSecuencia = "dbo.InsertarOrdenProduccionSecuencia";
        private const string ProcedimientoAlmacenadoActualizarEstado = "dbo.ActualizarEstadoOrdenProduccion";
        private const string ProcedimientoAlmacenadoActualizarPorRechazo = "dbo.ActualizarOrdenProduccionPorRechazo";
        private const string ProcedimientoAlmacenadoActualizarAsignacion = "dbo.ActualizarOrdenProduccionPorAsignacion";

        public IList<OrdenProduccion> ListarPorPrograma(int idPrograma)
        {
            var lista = new List<OrdenProduccion>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorPrograma);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenProduccion
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            OrdenVenta = new OrdenVenta
                            {
                                Id = GetDataValue<int>(lector, "IdOrdenVenta"),
                                Numero = GetDataValue<string>(lector, "NumeroOrdenVenta"),
                                FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                                Producto = new Producto
                                {
                                    Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                                }
                            }
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorPrograma);
            }
            return lista;
        }

        public IList<OrdenProduccion> ListarParaAsignar(int idPrograma, string estado1, string estado2)
        {
            var lista = new List<OrdenProduccion>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarParaAsginar);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);
                Context.Database.AddInParameter(comando, "@Estado1", DbType.String, estado1);
                Context.Database.AddInParameter(comando, "@Estado2", DbType.String, estado2);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenProduccion
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            OrdenVenta = new OrdenVenta
                            {
                                Id = GetDataValue<int>(lector, "IdOrdenVenta"),
                                Numero = GetDataValue<string>(lector, "NumeroOrdenVenta"),
                                FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                                Producto = new Producto
                                {
                                    Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                                }
                            },
                            Operador = new Usuario
                            {
                                Id = GetDataValue<int>(lector, "IdOperador"),
                                Username = GetDataValue<string>(lector, "UsernameOperador"),
                                NombreCompleto = GetDataValue<string>(lector, "NombreOperador"),
                            }
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarParaAsginar);
            }
            return lista;
        }

        public IList<OrdenProduccion> ListarParaEjecutar(string estado1)
        {
            var lista = new List<OrdenProduccion>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarParaEjecutar);

                Context.Database.AddInParameter(comando, "@Estado1", DbType.String, estado1);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenProduccion
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            OrdenVenta = new OrdenVenta
                            {
                                Id = GetDataValue<int>(lector, "IdOrdenVenta"),
                                Numero = GetDataValue<string>(lector, "NumeroOrdenVenta"),
                                FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                                Producto = new Producto
                                {
                                    Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                                }
                            },
                            Operador = new Usuario
                            {
                                Id = GetDataValue<int>(lector, "IdOperador"),
                                Username = GetDataValue<string>(lector, "UsernameOperador"),
                                NombreCompleto = GetDataValue<string>(lector, "NombreOperador"),
                            }
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarParaEjecutar);
            }
            return lista;
        }

        public OrdenProduccion ObetenerPorID(int id)
        {
            OrdenProduccion entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorID);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        entidad = new OrdenProduccion
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            CantidadProducto = GetDataValue<int>(lector, "CantidadProducto"),
                            OrdenVenta = new OrdenVenta
                            {
                                Id = GetDataValue<int>(lector, "IdOrdenVenta"),
                                Numero = GetDataValue<string>(lector, "NumeroOrdenVenta"),
                                Cliente = GetDataValue<string>(lector, "Cliente"),
                                Cantidad = GetDataValue<int>(lector, "CantidadOrdenVenta"),
                                FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                                Producto = new Producto
                                {
                                    Id = GetDataValue<int>(lector, "IdProducto"),
                                    Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                                }
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoObtenerPorID);
            }
            return entidad;
        }

        public OrdenProduccion ObetenerPorNumero(string numero)
        {
            OrdenProduccion entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorNumero);

                Context.Database.AddInParameter(comando, "@Numero", DbType.String, numero);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        entidad = new OrdenProduccion
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            CantidadProducto = GetDataValue<int>(lector, "CantidadProducto"),
                            OrdenVenta = new OrdenVenta
                            {
                                Id = GetDataValue<int>(lector, "IdOrdenVenta"),
                                Numero = GetDataValue<string>(lector, "NumeroOrdenVenta"),
                                Cliente = GetDataValue<string>(lector, "Cliente"),
                                Cantidad = GetDataValue<int>(lector, "CantidadOrdenVenta"),
                                FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                                Producto = new Producto
                                {
                                    Id = GetDataValue<int>(lector, "IdProducto"),
                                    Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                                }
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoObtenerPorNumero);
            }
            return entidad;
        }

        public IList<OrdenProduccionMaterial> ListarMaterial(int idOrdenProduccion)
        {
            var lista = new List<OrdenProduccionMaterial>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarMaterial);

                Context.Database.AddInParameter(comando, "@IdOrdenProduccion", DbType.Int32, idOrdenProduccion);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenProduccionMaterial
                        {
                            Requerido = GetDataValue<int>(lector, "Requerido"),
                            Comprar = GetDataValue<int>(lector, "Comprar"),
                            Material = new Material
                            {
                                Id = GetDataValue<int>(lector, "IdMaterial"),
                                Descripcion = GetDataValue<string>(lector, "Descripcion"),
                                Stock = GetDataValue<int>(lector, "Stock"),
                                StockMinimo = GetDataValue<int>(lector, "StockMinimo"),
                                Reservado = GetDataValue<int>(lector, "Reservado")
                            }
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarMaterial);
            }
            return lista;
        }

        public IList<OrdenProduccionSecuencia> ListarSecuencia(int idOrdenProduccion)
        {
            var lista = new List<OrdenProduccionSecuencia>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarSecuencia);

                Context.Database.AddInParameter(comando, "@IdOrdenProduccion", DbType.Int32, idOrdenProduccion);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenProduccionSecuencia
                        {
                            Secuencia = GetDataValue<int>(lector, "Secuencia"),
                            FechaInicio = GetDataValue<DateTime>(lector, "FechaInicio"),
                            FechaFin = GetDataValue<DateTime>(lector, "FechaFin"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            Maquina = new Maquina
                            {
                                Id = GetDataValue<int>(lector, "IdMaquina"),
                                Descripcion = GetDataValue<string>(lector, "Descripcion"),
                                PorcentajeFalla = GetDataValue<string>(lector, "PorcentajeFalla"),
                                Tiempo = GetDataValue<string>(lector, "Tiempo"),
                                Longitud = GetDataValue<int>(lector, "Longitud"),
                                Espesor = GetDataValue<int>(lector, "Espesor"),
                                Ciclo = GetDataValue<string>(lector, "Ciclo"),
                                PLD = GetDataValue<string>(lector, "PLD")
                            }
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarSecuencia);
            }
            return lista;
        }

        public int Registrar(OrdenProduccion ordenProduccion)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoInsertarOrdenProduccion);

                Context.Database.AddInParameter(comando, "@IdOrdenVenta", DbType.Int32, ordenProduccion.OrdenVenta.Id);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, ordenProduccion.Estado);
                Context.Database.AddInParameter(comando, "@CantidadProducto", DbType.Int32, ordenProduccion.CantidadProducto);

                var idOrdenProduccion = Convert.ToInt32(Context.ExecuteScalar(comando));

                return idOrdenProduccion;
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoInsertarOrdenProduccion);
            }
        }

        public void RegistrarMaterial(OrdenProduccionMaterial ordenProduccionMaterial)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoInsertarOrdenProduccionMaterial);

                Context.Database.AddInParameter(comando, "@IdOrdenProduccion", DbType.Int32, ordenProduccionMaterial.IdOrdenProduccion);
                Context.Database.AddInParameter(comando, "@IdMaterial", DbType.Int32, ordenProduccionMaterial.Material.Id);
                Context.Database.AddInParameter(comando, "@Requerido", DbType.Int32, ordenProduccionMaterial.Requerido);
                Context.Database.AddInParameter(comando, "@Comprar", DbType.Int32, ordenProduccionMaterial.Comprar);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoInsertarOrdenProduccionMaterial);
            }
        }

        public void RegistrarSecuencia(OrdenProduccionSecuencia ordenProduccionSecuencia)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoInsertarOrdenProduccionSecuencia);

                Context.Database.AddInParameter(comando, "@Secuencia", DbType.Int32, ordenProduccionSecuencia.Secuencia);
                Context.Database.AddInParameter(comando, "@IdOrdenProduccion", DbType.Int32, ordenProduccionSecuencia.IdOrdenProduccion);
                Context.Database.AddInParameter(comando, "@IdMaquina", DbType.Int32, ordenProduccionSecuencia.Maquina.Id);
                Context.Database.AddInParameter(comando, "@FechaInicio", DbType.DateTime, ordenProduccionSecuencia.FechaInicio);
                Context.Database.AddInParameter(comando, "@FechaFin", DbType.DateTime, ordenProduccionSecuencia.FechaFin);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, ordenProduccionSecuencia.Estado);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoInsertarOrdenProduccionSecuencia);
            }
        }

        public void ActualizarEstado(int id, string estado)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarEstado);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, estado);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarEstado);
            }
        }

        public void Rechazar(int id, string estado, string motivo)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarPorRechazo);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, estado);
                Context.Database.AddInParameter(comando, "@MotivoRechazo", DbType.String, motivo);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarPorRechazo);
            }
        }

        public void AsignarAsistentePlaneamiento(OrdenProduccion ordenProduccion)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarAsignacion);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, ordenProduccion.Id);
                Context.Database.AddInParameter(comando, "@IdOperador", DbType.Int32, ordenProduccion.Operador.Id);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, ordenProduccion.Estado);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarAsignacion);
            }
        }
    }
}
