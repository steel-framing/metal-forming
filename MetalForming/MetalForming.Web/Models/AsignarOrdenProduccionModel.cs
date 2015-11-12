using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class AsignarOrdenProduccionModel
    {
        public AsignarOrdenProduccionModel()
        {
            OrdenesProduccion = new List<OrdenProduccionModel>();
            Operadores = new List<UsuarioModel>();
        }

        public ProgramaModel ProgramaVigente { get; set; }

        public IList<OrdenProduccionModel> OrdenesProduccion { get; set; }

        public IList<UsuarioModel> Operadores { get; set; }
    }
}