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

        public IList<Material> ListarMateriales(string codigo, string descripcion, int tipo, int min, int max, int estado)
        {
            try
            {
                var lista = _materialDA.ListarMateriales(codigo, descripcion, tipo, min, max, estado);

                foreach (var item in lista)
                {
                    item.Informacion = string.Format("{0}/{1}/{2}/{3}", item.Presion, item.Ancho, item.Largo, item.Espesor);
                }

                return lista;
            }
            catch (Exception ex)
            {
                
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public string GuardarMaterial(Material material)
        {
            try
            {
                return _materialDA.GuardarMaterial(material);
            }
            catch (Exception ex)
            {
                
                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public string ActualizarMaterial(Material material)
        {
            try
            {
                return _materialDA.ActualizarMaterial(material);
            }
            catch (Exception ex)
            {

                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public string EliminarMaterial(int id)
        {
            try
            {
                return _materialDA.EliminarMaterial(id);
            }
            catch (Exception ex)
            {

                throw ThrowException(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
