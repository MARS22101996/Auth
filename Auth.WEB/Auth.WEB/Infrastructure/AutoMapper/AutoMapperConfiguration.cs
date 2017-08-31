using AutoMapper;

namespace Auth.WEB.Infrastructure.AutoMapper
{
	public class AutoMapperConfiguration
	{
		public MapperConfiguration Configure()
		{
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ApiModelToViewModelProfile>();
				cfg.AddProfile<ViewModelToApiModelProfile>();
			});

			return mapperConfiguration;
		}
	}
}
