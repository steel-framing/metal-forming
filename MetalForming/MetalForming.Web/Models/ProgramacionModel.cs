using MetalForming.Web.ProduccionService;
using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class ProgramacionModel
    {
        public ProgramacionModel()
        {
            OrdenesVenta = new List<OrdenVentaModel>();
            OrdenesProduccion = new List<OrdenProduccionModel>();
        }

        public ProgramaModel ProgramaVigente { get; set; }

        public IList<OrdenVentaModel> OrdenesVenta { get; set; }

        public IList<OrdenProduccionModel> OrdenesProduccion { get; set; }
    }
}