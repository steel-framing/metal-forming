using System;
using System.Data;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class ProductoDA : BaseData
    {
        private const string ProcedimientoAlmacenadoActualizarStock = "dbo.ActualizarStockProducto";

        public void ActualizarStock(int idProducto, int cantidad)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarStock);

                Context.Database.AddInParameter(comando, "@IdProducto", DbType.Int32, idProducto);
                Context.Database.AddInParameter(comando, "@Cantidad", DbType.Int32, cantidad);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizarStock);
            }
        }
    }
}
