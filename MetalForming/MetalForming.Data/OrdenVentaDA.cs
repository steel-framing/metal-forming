using System;
using System.Collections.Generic;
using System.Data;
using MetalForming.BusinessEntities;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class OrdenVentaDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListar = "dbo.ListarOrdenVenta";
        private const string ProcedimientoAlmacenadoListarPendientesPrograma = "dbo.ListarOrdenVentaPendientePrograma";
        private const string ProcedimientoAlmacenadoListarPorPrograma = "dbo.ListarOrdenVentaPorPrograma";
        private const string ProcedimientoAlmacenadoObtenerPorNumero = "dbo.ObtenerOrdenVentaPorNumero";
        private const string ProcedimientoAlmacenadoActualizarEstado = "dbo.ActualizarEstadoOrdenVenta";
        private const string ProcedimientoAlmacenadoActualizarPrograma = "dbo.ActualizarProgramaOrdenVenta";
        private const string ProcedimientoAlmacenadoActualizarProgramasHaciaNull = "dbo.ActualizarProgramasHaciaNullOrdenVenta";

        public IList<OrdenVenta> Listar()
        {
            var lista = new List<OrdenVenta>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListar);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenVenta
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Cliente = GetDataValue<string>(lector, "Cliente"),
                            FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            Cantidad = GetDataValue<int>(lector, "Cantidad"),
                            Producto = new Producto
                            {
                                Id = GetDataValue<int>(lector, "IdProducto"),
                                Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                            },
                            IdPrograma = GetDataValue<int>(lector, "IdPrograma")
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListar);
            }
            return lista;
        }

        public IList<OrdenVenta> ListarPendientePrograma()
        {
            var lista = new List<OrdenVenta>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPendientesPrograma);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenVenta
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Cliente = GetDataValue<string>(lector, "Cliente"),
                            FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            Cantidad = GetDataValue<int>(lector, "Cantidad"),
                            Producto = new Producto
                            {
                                Id = GetDataValue<int>(lector, "IdProducto"),
                                Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                            },
                            IdPrograma = GetDataValue<int>(lector, "IdPrograma")
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPendientesPrograma);
            }
            return lista;
        }

        public IList<OrdenVenta> ListarPorPrograma(int idPrograma)
        {
            var lista = new List<OrdenVenta>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorPrograma);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new OrdenVenta
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Cliente = GetDataValue<string>(lector, "Cliente"),
                            FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            Cantidad = GetDataValue<int>(lector, "Cantidad"),
                            Producto = new Producto
                            {
                                Id = GetDataValue<int>(lector, "IdProducto"),
                                Descripcion = GetDataValue<string>(lector, "DescripcionProducto")
                            },
                            IdPrograma = GetDataValue<int>(lector, "IdPrograma")
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

        public OrdenVenta ObtenerPorNumero(string numero)
        {
            OrdenVenta entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorNumero);

                Context.Database.AddInParameter(comando, "@Numero", DbType.String, numero);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        entidad = new OrdenVenta
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            Cliente = GetDataValue<string>(lector, "Cliente"),
                            FechaEntrega = GetDataValue<DateTime>(lector, "FechaEntrega"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            Cantidad = GetDataValue<int>(lector, "Cantidad"),
                            Producto = new Producto
                            {
                                Id = GetDataValue<int>(lector, "IdProducto"),
                                Descripcion = GetDataValue<string>(lector, "DescripcionProducto"),
                                Stock = GetDataValue<int>(lector, "StockProducto"),
                                StockMinimo = GetDataValue<int>(lector, "StockMinimoProducto")
                            },
                            IdPrograma = GetDataValue<int>(lector, "IdPrograma")
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

        public void ActualizarPrograma(int id, int idPrograma, string estado)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarPrograma);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);
                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, estado);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarPrograma);
            }
        }

        public void ActualizarProgramasHaciaNull(int idPrograma, string estado)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarProgramasHaciaNull);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, estado);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarProgramasHaciaNull);
            }
        }
    }
}
