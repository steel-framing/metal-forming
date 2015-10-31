using System;
using System.Collections.Generic;
using System.Data;
using MetalForming.BusinessEntities;
using MetalForming.Data.Core;

namespace MetalForming.Data
{
    public class MaquinaDA : BaseData
    {
        private const string ProcedimientoAlmacenadoListarPorBusqueda = "dbo.ListarMaquinaPorBusqueda";
        private const string ProcedimientoAlmacenadoObtenerPorID = "dbo.ObtenerMaquinaPorID";

        public IList<Maquina> ListarPorBusqueda(string descripcion, string tipo, string pld, string configuracion)
        {
            var lista = new List<Maquina>();
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoListarPorBusqueda);

                Context.Database.AddInParameter(comando, "@Descripcion", DbType.String, descripcion);
                Context.Database.AddInParameter(comando, "@Tipo", DbType.String, tipo);
                Context.Database.AddInParameter(comando, "@PLD", DbType.String, pld);
                Context.Database.AddInParameter(comando, "@Configuracion", DbType.String, configuracion);

                using (var lector = Context.ExecuteReader(comando))
                {
                    while (lector.Read())
                    {
                        var entidad = new Maquina
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Descripcion = GetDataValue<string>(lector, "Descripcion"),
                            Tipo = GetDataValue<string>(lector, "Tipo"),
                            PLD = GetDataValue<string>(lector, "PLD"),
                            Configuracion = GetDataValue<string>(lector, "Configuracion"),
                            PorcentajeFalla = GetDataValue<string>(lector, "PorcentajeFalla"),
                            Tiempo = GetDataValue<string>(lector, "Tiempo")
                        };

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

        public Maquina ObtenerPorID(int id)
        {
            Maquina entidad = null;
            try
            {
                var comando = Context.Database.GetStoredProcCommand(ProcedimientoAlmacenadoObtenerPorID);

                Context.Database.AddInParameter(comando, "@Id", DbType.Int32, id);

                using (var lector = Context.ExecuteReader(comando))
                {
                    if (lector.Read())
                    {
                        entidad = new Maquina
                        {
                            Id = GetDataValue<int>(lector, "Id"),
                            Descripcion = GetDataValue<string>(lector, "Descripcion"),
                            Tipo = GetDataValue<string>(lector, "Tipo"),
                            PLD = GetDataValue<string>(lector, "PLD"),
                            Configuracion = GetDataValue<string>(lector, "Configuracion"),
                            Estado = GetDataValue<string>(lector, "Estado"),
                            ReduacionInicio = GetDataValue<string>(lector, "ReduacionInicio"),
                            ReduacionFin = GetDataValue<string>(lector, "ReduacionFin"),
                            CantidadRodillos = GetDataValue<string>(lector, "CantidadRodillos"),
                            MaximoFrio = GetDataValue<string>(lector, "MaximoFrio"),
                            MaximoCaliente = GetDataValue<string>(lector, "MaximoCaliente"),
                            PorcentajeFalla = GetDataValue<string>(lector, "PorcentajeFalla"),
                            Tiempo = GetDataValue<string>(lector, "Tiempo")
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
    }
}
