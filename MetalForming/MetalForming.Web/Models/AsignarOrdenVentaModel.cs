using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class AsignarOrdenVentaModel
    {
        public AsignarOrdenVentaModel()
        {
            OrdenesVenta = new List<OrdenVentaModel>();
            AsistentePlaneamiento = new List<UsuarioModel>();
        }

        public ProgramaModel ProgramaVigente { get; set; }

        public IList<OrdenVentaModel> OrdenesVenta { get; set; }

        public IList<UsuarioModel> AsistentePlaneamiento { get; set; }
    }
}