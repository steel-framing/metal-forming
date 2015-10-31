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
        private const string ProcedimientoAlmacenadoObtenerPorNumero = "dbo.ObtenerOrdenVentaPorNumero";
        private const string ProcedimientoAlmacenadoActualizarEstado = "dbo.ActualizarEstadoOrdenVenta";

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
    }
}
