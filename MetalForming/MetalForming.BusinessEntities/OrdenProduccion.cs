using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class OrdenProduccion : BaseEntity
    {
        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public OrdenVenta OrdenVenta { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public IList<OrdenProduccionMaterial> Materiales { get; set; }

        [DataMember]
        public IList<OrdenProduccionSecuencia> Secuencia { get; set; }

        [DataMember]
        public bool TomarStock { get; set; }

        [DataMember]
        public int CantidadProducto { get; set; }
    }
}
