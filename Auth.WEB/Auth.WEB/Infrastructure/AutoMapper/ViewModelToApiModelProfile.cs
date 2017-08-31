using Auth.WEB.ApiModels;
using Auth.WEB.ViewModels.AccountViewModel;
using AutoMapper;

namespace Auth.WEB.Infrastructure.AutoMapper
{
	public class ViewModelToApiModelProfile : Profile
	{
		public ViewModelToApiModelProfile()
		{
			CreateMap<UserViewModel, UserApiModel>();
		}
	}
}