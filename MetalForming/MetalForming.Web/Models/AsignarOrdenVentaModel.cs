using MetalForming.Web.ProduccionService;
using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class AsignarOrdenVentaModel
    {
        public AsignarOrdenVentaModel()
        {
            OrdenesVenta = new List<OrdenVentaModel>();
        }

        public ProgramaModel ProgramaVigente { get; set; }

        public IList<OrdenVentaModel> OrdenesVenta { get; set; }

        public IList<AsistentePlaneamiento> AsistentePlaneamiento { get; set; }
    }
}