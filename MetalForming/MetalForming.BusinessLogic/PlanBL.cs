using System;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;

namespace MetalForming.BusinessLogic
{
    public class PlanBL : BaseLogic
    {
        private readonly PlanDA _palDA = new PlanDA();

        public Plan ObtenerVigente()
        {
            try
            {
                return _palDA.ObtenerVigente();
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
