using System;
using System.Collections.Generic;
using System.Data;
using MetalForming.BusinessEntities;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class ProgramaDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorPlan = "dbo.ListarProgramaPorPlan";

        public IList<Programa> ListrarPorPlan(int idPlan)
        {
            IList<Programa> lista = new List<Programa>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorPlan);

                Context.Database.AddInParameter(comando, "@IdPlan", DbType.Int32, idPlan);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        var entidad = new Programa
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            FechaInicio = GetDataValue<DateTime>(lector, "FechaInicio"),
                            FechaFin = GetDataValue<DateTime>(lector, "FechaFin"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            CantidadOV = GetDataValue<int>(lector, "CantidadOV"),
                            IdPlan = GetDataValue<int>(lector, "IdPlan")
                        };

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorPlan);
            }
            return lista;
        }

        
    }
}
