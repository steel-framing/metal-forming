using MetalForming.BusinessEntities;
using MetalForming.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace MetalForming.Data
{
    public class OrdenProduccionDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListar = "dbo.ListarOrdenProduccion";
        private const string ProcedimientoAlmacenadoListarPorPrograma = "dbo.ListarOrdenProduccionPorPrograma";
        private const string ProcedimientoAlmacenadoObtenerPorNumero = "dbo.ObtenerOrdenProduccionPorNumero";
        private const string ProcedimientoAlmacenadoListarMaterial = "dbo.ListarOrdenProduccionMaterial";
        private const string ProcedimientoAlmacenadoListarSecuencia = "dbo.ListarOrdenProduccionSecuencia";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccion = "dbo.InsertarOrdenProduccion";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccionMaterial = "dbo.InsertarOrdenProduccionMaterial";
        private const string ProcedimientoAlmacenadoInsertarOrdenProduccionSecuencia = "dbo.InsertarOrdenProduccionSecuencia";

        public IList<OrdenProduccion> Listar()
        {
            var lista = new List<OrdenProduccion>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListar);

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
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListar);
            }
            return lista;
        }

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
                            Maquina = new Maquina
                            {
                                Id = GetDataValue<int>(lector, "IdMaquina"),
                                Descripcion = GetDataValue<string>(lector, "Descripcion"),
                                PorcentajeFalla = GetDataValue<string>(lector, "PorcentajeFalla"),
                                Tiempo = GetDataValue<string>(lector, "Tiempo")
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

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoInsertarOrdenProduccionSecuencia);
            }
        }
    }
}
