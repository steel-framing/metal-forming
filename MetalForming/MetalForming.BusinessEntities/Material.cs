using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Material : BaseEntity
    {
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
