namespace MetalForming.Common
{
    public class Constantes
    {
        public class EstadoPlan
        {
            public const string Pendiente = "Pendiente";
        }

        public class EstadoOrdenVenta
        {
            public const string Pendiente = "Pendiente";
            public const string Programado = "Programado";
        }

        public class EstadoOrdenPoduccion
        {
            public const string Programado = "Programado";
            public const string ReProgramado = "ReProgramado";
            public const string EnProceso = "En Proceso";
            public const string Producido = "Producido";
        }
    }
}
