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
        public string Presion { get; set; }
        [DataMember]
        public string Ancho { get; set; }
        [DataMember]
        public string Largo { get; set; }
        [DataMember]
        public string Espesor { get; set; }
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
    }
}
