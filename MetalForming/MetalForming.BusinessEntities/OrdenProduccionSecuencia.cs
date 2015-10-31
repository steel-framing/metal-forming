using System;
using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    public class OrdenProduccionSecuencia : BaseEntity
    {
        [DataMember]
        public int IdOrdenProduccion { get; set; }

        [DataMember]
        public int Secuencia { get; set; }

        [DataMember]
        public Maquina Maquina { get; set; }

        [DataMember]
        public DateTime FechaInicio { get; set; }

        [DataMember]
        public DateTime FechaFin { get; set; }
    }
}
