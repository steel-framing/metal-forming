using System;
using System.Collections.Generic;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;

namespace MetalForming.BusinessLogic
{
    public class ProgramaBL : BaseLogic
    {
        private readonly ProgramaDA _programaDA = new ProgramaDA();

        public IList<Programa> ListrarPorPlan(int idPlan)
        {
            try
            {
                return _programaDA.ListrarPorPlan(idPlan);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
