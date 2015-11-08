using System;
using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Plan : BaseEntity
    {
        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public DateTime FechaInicio { get; set; }

        [DataMember]
        public DateTime FechaFin { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
