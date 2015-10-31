namespace MetalForming.BusinessEntities.Core
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class BaseEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}
