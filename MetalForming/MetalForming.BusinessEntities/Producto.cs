using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Producto : BaseEntity
    {
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public int StockMinimo { get; set; }
    }
}
