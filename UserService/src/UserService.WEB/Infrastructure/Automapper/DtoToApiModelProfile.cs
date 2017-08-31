using AutoMapper;
using UserService.BLL.DTO;
using UserService.WEB.Models.AccountApiModels;

namespace UserService.WEB.Infrastructure.Automapper
{
    public class DtoToApiModelProfile : Profile
    {
        public DtoToApiModelProfile()
        {
            CreateMap<RegisterModelDto, RegisterApiModel>();
            CreateMap<LoginModelDto, LoginApiModel>();
        }
    }
}