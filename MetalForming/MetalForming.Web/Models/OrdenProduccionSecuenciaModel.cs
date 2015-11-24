using System;

namespace MetalForming.Web.Models
{
    public class OrdenProduccionSecuenciaModel
    {
        public int Secuencia { get; set; }

        public int IdMaquina { get; set; }

        public string DescripcionMaquina { get; set; }

        public string PorcentajeFalla { get; set; }

        public string Tiempo { get; set; }

        public string FechaInicioStr { get; set; }

        public DateTime FechaInicio { get; set; }

        public string FechaFinStr { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; }

        public string PLC { get; set; }
    }
}
