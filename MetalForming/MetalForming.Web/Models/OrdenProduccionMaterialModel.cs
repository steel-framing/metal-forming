namespace MetalForming.Web.Models
{
    public class OrdenProduccionMaterialModel
    {
        public int IdMaterial { get; set; }

        public string DescripcionMaterial { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        public int Requerido { get; set; }

        public int Reservado { get; set; }

        public int Comprar { get; set; }
    }
}
