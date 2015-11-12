using MetalForming.BusinessEntities;
using MetalForming.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace MetalForming.Data
{
    public class UsuarioDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorRol = "dbo.ListarUsuarioPorRol";

        public IList<Usuario> ListarPorRol(int idRol)
        {
            var lista = new List<Usuario>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorRol);

                Context.Database.AddInParameter(comando, "@IdRol", DbType.Int32, idRol);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new Usuario
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            IdRol = GetDataValue<int>(lector, "IdRol"),
                            Username = GetDataValue<string>(lector, "Username"),
                            Password = GetDataValue<string>(lector, "Password"),
                            NombreCompleto = GetDataValue<string>(lector, "NombreCompleto"),
                            Estado = GetDataValue<bool>(lector, "Estado"),
                            CantidadOV = GetDataValue<int>(lector, "CantidadOV"),
                            CantidadOP = GetDataValue<int>(lector, "CantidadOP")
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorRol);
            }
            return lista;
        }
    }
}
