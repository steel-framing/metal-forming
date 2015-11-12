using System.Runtime.Serialization;
using MetalForming.BusinessEntities.Core;

namespace MetalForming.BusinessEntities
{
    [DataContract]
    public class Usuario : BaseEntity
    {
        [DataMember]
        public int IdRol { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string NombreCompleto { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool Estado { get; set; }

        [DataMember]
        public int CantidadOV { get; set; }

        [DataMember]
        public int CantidadOP { get; set; }
    }
}
