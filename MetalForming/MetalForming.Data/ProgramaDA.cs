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
        private const string ProcedimientoAlmacenadoObtenerPorID = "dbo.ObtenerProgramaPorID";
        private const string ProcedimientoAlmacenadoObtenerPorEstado = "dbo.ObtenerProgramaPorEstado";
        private const string ProcedimientoAlmacenadoRegistrar = "dbo.InsertarPrograma";
        private const string ProcedimientoAlmacenadoActualizar = "dbo.ActualizarPrograma";
        private const string ProcedimientoAlmacenadoActualizarEstado = "dbo.ActualizarEstadoPrograma";

        public IList<Programa> ListrarPorPlan(int idPlan)
        {
            IList<Programa> lista = new List<Programa>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorPlan);

                Context.Database.AddInParameter(comando, "@IdPlan", DbType.Int32, idPlan);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
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

        public Programa ObtenerPorID(int id)
        {
            Programa entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorID);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);

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
                            Estado = GetDataValue<string>(lector, "Estado"),
                            CantidadOV = GetDataValue<int>(lector, "CantidadOV"),
                            IdPlan = GetDataValue<int>(lector, "IdPlan")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoObtenerPorID);
            }
            return entidad;
        }

        public Programa ObtenerPorEstado(string estado)
        {
            Programa entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorEstado);

                Context.Database.AddInParameter(comando, "@Estado", DbType.String, estado);

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
                            Estado = GetDataValue<string>(lector, "Estado"),
                            CantidadOV = GetDataValue<int>(lector, "CantidadOV"),
                            IdPlan = GetDataValue<int>(lector, "IdPlan")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoObtenerPorEstado);
            }
            return entidad;
        }

        public void Registrar(Programa programa)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoRegistrar);

                Context.Database.AddOutParameter(comando, "@IdPrograma", DbType.Int32, 7);
                Context.Database.AddOutParameter(comando, "@Numero", DbType.String, 50);
                Context.Database.AddInParameter(comando, "@FechaInicio", DbType.Date, programa.FechaInicio);
                Context.Database.AddInParameter(comando, "@FechaFin", DbType.Date, programa.FechaFin);
                Context.Database.AddInParameter(comando, "@CantidadOV", DbType.Int32, programa.CantidadOV);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, programa.Estado);
                Context.Database.AddInParameter(comando, "@IdPlan", DbType.Int32, programa.IdPlan);

                Context.ExecuteNonQuery(comando);

                programa.Id = Convert.ToInt32(Context.Database.GetParameterValue(comando, "@IdPrograma"));
                programa.Numero = Convert.ToString(Context.Database.GetParameterValue(comando, "@Numero"));
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoRegistrar);
            }
        }

        public void Actualizar(Programa programa)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizar);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, programa.Id);
                Context.Database.AddInParameter(comando, "@FechaInicio", DbType.Date, programa.FechaInicio);
                Context.Database.AddInParameter(comando, "@FechaFin", DbType.Date, programa.FechaFin);
                Context.Database.AddInParameter(comando, "@CantidadOV", DbType.Int32, programa.CantidadOV);
                Context.Database.AddInParameter(comando, "@Estado", DbType.String, programa.Estado);
                Context.Database.AddInParameter(comando, "@MotivoFinalizacion", DbType.String, programa.MotivoFinalizacion);
                Context.Database.AddInParameter(comando, "@FechaFinalizacion", DbType.Date, programa.FechaFinalizacion);

                Context.ExecuteNonQuery(comando);
            }
            catch (Exception ex)
            {
                throw new ExceptionData(ex.Message, Context.ProfileName, ProcedimientoAlmacenadoActualizar);
            }
        }

        public void ActualizarEstado(int idPrograma, string estado)
        {
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoActualizarEstado);

                Context.Database.AddInParameter(comando, "@IdPrograma", DbType.Int32, idPrograma);
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
