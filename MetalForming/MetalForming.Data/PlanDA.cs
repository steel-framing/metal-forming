using System;
using MetalForming.BusinessEntities;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class PlanDA : BaseData
    {
        private const string ProcedimientoAlmacenadoObtenerVigente = "dbo.ObtenerPlanVigente";

        public Plan ObtenerVigente()
        {
            Plan entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerVigente);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        entidad = new Plan
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Codigo = GetDataValue<string>(lector, "Codigo"),
                            FechaInicio = GetDataValue<DateTime>(lector, "FechaInicio"),
                            FechaFin = GetDataValue<DateTime>(lector, "FechaFin"),
                            Estado = GetDataValue<string>(lector, "Estado")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoObtenerVigente);
            }
            return entidad;
        }
    }
}
