using AutoMapper;
using UserService.BLL.Infrastructure.Automapper;

namespace UserService.WEB.Infrastructure.Automapper
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToApiModelProfile>();
                cfg.AddProfile<ApiModelToDtoProfile>();

                ServiceAutoMapperConfiguration.Initialize(cfg);
            });

            return mapperConfiguration;
        }
    }
}