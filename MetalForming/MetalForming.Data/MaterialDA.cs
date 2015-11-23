using System;
using System.Collections.Generic;
using System.Data;
using MetalForming.BusinessEntities;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class MaterialDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorProducto = "dbo.ListarMaterialPorProducto";
        private const string ProcedimientoAlmacenadoListarPorBusqueda = "dbo.ListarMaterialPorBusqueda";
        private const string ProcedimientoAlmacenadoGuardar = "dbo.InsertarMaterial";
        private const string ProcedimientoAlmacenadoActualizar = "dbo.ActualizarMaterial";
        private const string ProcedimientoAlmacenadoEliminar = "dbo.EliminarMaterial";
        private const string ProcedimientoAlmacenadoActualizarStock = "dbo.ActualizarStockMaterial";

        public IList<Material> ListarPorProducto(int idProducto)
        {
            var lista = new List<Material>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorProducto);

                Context.Database.AddInParameter(comando, "@IdProducto", DbType.Int32, idProducto);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new Material
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Descripcion = GetDataValue<string>(lector, "Descripcion"),
                            Stock = GetDataValue<int>(lector, "Stock"),
                            StockMinimo = GetDataValue<int>(lector, "StockMinimo"),
                            Reservado = GetDataValue<int>(lector, "Reservado"),
                            Cantidad = GetDataValue<int>(lector, "Cantidad")
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorProducto);
            }
            return lista;
        }

        public IList<Material> ListarMateriales(string codigo, string descripcion, int tipo, int min, int max, int estado)
        {
            var lista = new List<Material>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorBusqueda);

                Context.Database.AddInParameter(comando, "@Codigo", DbType.String, codigo);
                Context.Database.AddInParameter(comando, "@Descripcion", DbType.String, descripcion);
                Context.Database.AddInParameter(comando, "@Tipo", DbType.Int32, tipo);
                Context.Database.AddInParameter(comando, "@Min", DbType.Int32, min);
                Context.Database.AddInParameter(comando, "@Max", DbType.Int32, max);
                Context.Database.AddInParameter(comando, "@Estado", DbType.Int32, estado);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new Material();
                        entidad.Id = GetDataValue<int>(lector, "Id");
                        entidad.Codigo = GetDataValue<string>(lector, "Codigo");
                        entidad.Descripcion = GetDataValue<string>(lector, "Descripcion");
                        entidad.Presion = GetDataValue<int>(lector, "Presion");
                        entidad.Ancho = GetDataValue<int>(lector, "Ancho");
                        entidad.Largo = GetDataValue<int>(lector, "Largo");
                        entidad.Espesor = GetDataValue<int>(lector, "Espesor");
                        entidad.Estado = GetDataValue<int>(lector, "Estado");

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorBusqueda);
            }
            return lista;
        }

        public string GuardarMaterial(Material material)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoGuardar);

                Context.Database.AddInParameter(comando, "@Codigo", DbType.String, material.Codigo);
                Context.Database.AddInParameter(comando, "@Descripcion", DbType.String, material.Descripcion);
                Context.Database.AddInParameter(comando, "@Presion", DbType.Int32, material.Presion);
                Context.Database.AddInParameter(comando, "@Ancho", DbType.Int32, material.Ancho);
                Context.Database.AddInParameter(comando, "@Largo", DbType.Int32, material.Largo);
                Context.Database.AddInParameter(comando, "@Espesor", DbType.Int32, material.Espesor);
                Context.Database.AddInParameter(comando, "@StockMinimo", DbType.Int32, material.StockMinimo);
                Context.Database.AddInParameter(comando, "@Estado", DbType.Int32, material.Estado);

                var result = Convert.ToInt32(Context.ExecuteScalar(comando));

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoGuardar);
            }
        }

        public string ActualizarMaterial(Material material)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizar);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, material.Id);
                Context.Database.AddInParameter(comando, "@Codigo", DbType.String, material.Codigo);
                Context.Database.AddInParameter(comando, "@Descripcion", DbType.String, material.Descripcion);
                Context.Database.AddInParameter(comando, "@Presion", DbType.Int32, material.Presion);
                Context.Database.AddInParameter(comando, "@Ancho", DbType.Int32, material.Ancho);
                Context.Database.AddInParameter(comando, "@Largo", DbType.Int32, material.Largo);
                Context.Database.AddInParameter(comando, "@Espesor", DbType.Int32, material.Espesor);
                Context.Database.AddInParameter(comando, "@StockMinimo", DbType.Int32, material.StockMinimo);
                Context.Database.AddInParameter(comando, "@Estado", DbType.Int32, material.Estado);

                var result = Convert.ToInt32(Context.ExecuteScalar(comando));

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizar);
            }
        }

        public string EliminarMaterial(int id)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoEliminar);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);
                var result = Convert.ToInt32(Context.ExecuteScalar(comando));

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoEliminar);
            }
        }

        public void ActualizarStock(int id, int cantidad)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarStock);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);
                Context.Database.AddInParameter(comando, "@Cantidad", DbType.Int32, cantidad);

                Context.Database.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarStock);
            }
        }
    }
}
