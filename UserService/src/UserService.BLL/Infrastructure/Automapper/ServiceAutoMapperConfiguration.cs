using AutoMapper;

namespace UserService.BLL.Infrastructure.Automapper
{
    public class ServiceAutoMapperConfiguration
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile(new DtoToEntityProfile());
            cfg.AddProfile(new EntityToDtoProfile());
        }
    }
}