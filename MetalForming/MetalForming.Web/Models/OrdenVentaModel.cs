using System;

namespace MetalForming.Web.Models
{
    public class OrdenVentaModel
    {
        public int Id { get; set; }

        public string Numero { get; set; }

        public string Cliente { get; set; }

        public DateTime FechaEntrega { get; set; }

        public string Estado { get; set; }

        public int IdPrograma { get; set; }

        public string NombreAsistentePlaneamiento { get; set; }
    }
}