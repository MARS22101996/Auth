using Auth.WEB.ApiModels;
using Auth.WEB.ViewModels.AccountViewModel;
using AutoMapper;

namespace Auth.WEB.Infrastructure.AutoMapper
{
	public class ApiModelToViewModelProfile : Profile
	{
		public ApiModelToViewModelProfile()
		{
			CreateMap<UserApiModel, UserViewModel>();
		}
	}
}