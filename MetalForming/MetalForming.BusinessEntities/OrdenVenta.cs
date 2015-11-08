using System;
using MetalForming.BusinessEntities.Core;
using System.Runtime.Serialization;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class OrdenVenta : BaseEntity
    {
        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public string Cliente { get; set; }

        [DataMember]
        public DateTime FechaEntrega { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public Producto Producto { get; set; }

        [DataMember]
        public int IdPrograma { get; set; }
    }
}
