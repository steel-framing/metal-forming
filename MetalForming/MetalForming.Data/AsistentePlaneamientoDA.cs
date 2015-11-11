using MetalForming.BusinessEntities;
using MetalForming.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace MetalForming.Data
{
    public class AsistentePlaneamientoDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListar = "dbo.ListarAsistentePlaneamiento";

        public IList<AsistentePlaneamiento> Listar()
        {
            var lista = new List<AsistentePlaneamiento>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListar);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new AsistentePlaneamiento
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Nombre = GetDataValue<string>(lector, "Nombre"),
                            Usuario = GetDataValue<string>(lector, "Usuario")
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
    }
}
