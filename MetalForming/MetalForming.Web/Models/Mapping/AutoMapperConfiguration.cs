namespace MetalForming.Web.Models.Mapping
{
    using AutoMapper;

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToModelMappingProfile>();
                x.AddProfile<ModelToEntityMappingProfile>();
            });
        }
    }
}
