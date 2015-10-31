using MetalForming.BusinessEntities.Core;
using System.Runtime.Serialization;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class OrdenProduccionMaterial : BaseEntity
    {
        [DataMember]
        public int IdOrdenProduccion { get; set; }

        [DataMember]
        public Material Material { get; set; }

        [DataMember]
        public int Requerido { get; set; }

        [DataMember]
        public int Comprar { get; set; }
    }
}
