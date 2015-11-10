using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class AsistentePlaneamiento : BaseEntity
    {
        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Nombre { get; set; }
    }
}
