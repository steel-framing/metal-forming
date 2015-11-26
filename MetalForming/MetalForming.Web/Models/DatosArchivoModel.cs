using System.Collections.Generic;

namespace MetalForming.Web.Models
{
    public class DatosArchivoModel
    {
        public string Maquina { get; set; }
        public string FechaInicioProduccion { get; set; }
        public string FechaFinProduccion { get; set; }
        public string Longitud { get; set; }
        public string Espesor { get; set; }
        public string Ciclo { get; set; }
        public string NoCiclos { get; set; }
        public string MotivosDeParada { get; set; }
        public string TiempoParada { get; set; }
        public string TiempoProduccion { get; set; }
        public string UnidadesAProducidas { get; set; }
        public string UnidadesProducidas { get; set; }
        public string UnidadesDefectuosas { get; set; }
        public IList<ParadaModel> Paradas { get; set; }
    }
}