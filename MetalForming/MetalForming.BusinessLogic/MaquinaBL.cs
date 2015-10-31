using System;
using System.Collections.Generic;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;

namespace MetalForming.BusinessLogic
{
    public class MaquinaBL : BaseLogic
    {
        private readonly MaquinaDA _maquinaDA = new MaquinaDA();

        public IList<Maquina> ListarPorBusqueda(string descripcion, string tipo, string pld, string configuracion)
        {
            try
            {
                return _maquinaDA.ListarPorBusqueda(descripcion, tipo, pld, configuracion);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public Maquina ObtenerPorID(int id)
        {
            try
            {
                return _maquinaDA.ObtenerPorID(id);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
