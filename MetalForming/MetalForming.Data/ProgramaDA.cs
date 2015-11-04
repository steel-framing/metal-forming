using MetalForming.BusinessEntities;
using MetalForming.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalForming.Data
{
    public class ProgramaDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorPlan = "dbo.ListarProgramaPorPlan";

        public Programa ListrarPorPlan(int idPlan)
        {
            Programa entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorPlan);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        entidad = new Programa
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Numero = GetDataValue<string>(lector, "Numero"),
                            FechaInicio = GetDataValue<DateTime>(lector, "FechaInicio"),
                            FechaFin = GetDataValue<DateTime>(lector, "FechaFin"),
                            Estado = GetDataValue<string>(lector, "Estado")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoListarPorPlan);
            }
            return entidad;
        }
    }
}
