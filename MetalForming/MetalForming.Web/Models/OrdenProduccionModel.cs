using System;
using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class OrdenProduccionModel
    {
        public OrdenProduccionModel()
        {
            Materiales = new List<OrdenProduccionMaterialModel>();
            Secuencia = new List<OrdenProduccionSecuenciaModel>();
        }

        public int Id { get; set; }

        public string Numero { get; set; }

        public int IdOrdenVenta { get; set; }

        public string NumeroOrdenVenta { get; set; }

        public DateTime FechaEntrega { get; set; }

        public int CantidadOrdenVenta { get; set; }

        public int IdProducto { get; set; }

        public int CantidadProducto { get; set; }

        public string DescripcionProducto { get; set; }

        public int StockProducto { get; set; }

        public int StockMinimoProducto { get; set; }

        public IList<OrdenProduccionMaterialModel> Materiales { get; set; }

        public IList<OrdenProduccionSecuenciaModel> Secuencia { get; set; }

        public bool TomarStock { get; set; }

        public string Estado { get; set; }

        public int IdOperador { get; set; }

        public string NombreOperador { get; set; }
    }
}
