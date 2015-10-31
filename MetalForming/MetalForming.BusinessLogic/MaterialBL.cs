using System;
using System.Collections.Generic;
using System.Reflection;
using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;

namespace MetalForming.BusinessLogic
{
    public class MaterialBL : BaseLogic
    {
        private readonly MaterialDA _materialDA = new MaterialDA();

        public IList<Material> ListarPorProducto(int idProducto)
        {
            try
            {
                return _materialDA.ListarPorProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
