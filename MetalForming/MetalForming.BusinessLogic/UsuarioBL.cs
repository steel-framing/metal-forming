using MetalForming.BusinessEntities;
using MetalForming.BusinessLogic.Core;
using MetalForming.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using MetalForming.Common;

namespace MetalForming.BusinessLogic
{
    public class UsuarioBL : BaseLogic
    {
        private readonly UsuarioDA _usuarioDA = new UsuarioDA();

        public IList<Usuario> ListarAsistentesPlaneamiento()
        {
            try
            {
                return _usuarioDA.ListarPorRol((int)TipoRol.AsistenteDePlaneamiento);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public IList<Usuario> ListarOperadores()
        {
            try
            {
                return _usuarioDA.ListarPorRol((int)TipoRol.Operador);
            }
            catch (Exception ex)
            {
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
