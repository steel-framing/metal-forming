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
    }
}
