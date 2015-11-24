using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Maquina : BaseEntity
    {
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public string PLD { get; set; }

        [DataMember]
        public string Configuracion { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string ReduacionInicio { get; set; }

        [DataMember]
        public string ReduacionFin { get; set; }

        [DataMember]
        public string CantidadRodillos { get; set; }

        [DataMember]
        public string MaximoFrio { get; set; }

        [DataMember]
        public string MaximoCaliente { get; set; }

        [DataMember]
        public string PorcentajeFalla { get; set; }

        [DataMember]
        public string Tiempo { get; set; }

        [DataMember]
        public int Longitud { get; set; }

        [DataMember]
        public int Espesor { get; set; }

        [DataMember]
        public string Ciclo { get; set; }
    }
}
