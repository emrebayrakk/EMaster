using EMaster.Domain.Entities;
using EMaster.Domain.Responses;
using Mapster;

namespace EMaster.Data.Mappings
{
    public static class MapsterConfiguration
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<User, UserResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Email, src => src.Email);
        }
    }
}
