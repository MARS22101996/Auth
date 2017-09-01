using AuthHost.BLL.Infrastructure.Automapper;
using AutoMapper;

namespace AuthHost.WEB.Infrastructure.Automapper
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