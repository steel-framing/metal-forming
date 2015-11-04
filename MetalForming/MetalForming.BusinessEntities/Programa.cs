using System;
using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Programa : BaseEntity
    {
        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public DateTime FechaInicio { get; set; }

        [DataMember]
        public DateTime FechaFin { get; set; }

        [DataMember]
        public int CantidadOV { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public int IdPlan { get; set; }
    }
}
