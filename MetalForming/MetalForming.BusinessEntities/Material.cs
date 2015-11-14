using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Material : BaseEntity
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public int Presion { get; set; }
        [DataMember]
        public int Ancho { get; set; }
        [DataMember]
        public int Largo { get; set; }
        [DataMember]
        public int Espesor { get; set; }
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public int StockMinimo { get; set; }

        [DataMember]
        public int Reservado { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public string Informacion { get; set; }
    }
}
