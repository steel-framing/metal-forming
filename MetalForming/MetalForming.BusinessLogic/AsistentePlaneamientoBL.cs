using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MetalForming.BusinessLogic
{
    public class AsistentePlaneamientoBL : BaseLogic
    {
        private readonly AsistentePlaneamientoDA _materialDA = new AsistentePlaneamientoDA();

        public IList<AsistentePlaneamiento> Listar()
        {
            try
            {
                return _materialDA.Listar();
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
