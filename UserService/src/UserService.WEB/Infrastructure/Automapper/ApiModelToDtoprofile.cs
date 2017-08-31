using AutoMapper;
using UserService.BLL.DTO;
using UserService.WEB.Models.AccountApiModels;

namespace UserService.WEB.Infrastructure.Automapper
{
    public class ApiModelToDtoProfile : Profile
    {
        public ApiModelToDtoProfile()
        {
            CreateMap<LoginApiModel, LoginModelDto>();
            CreateMap<RegisterApiModel, RegisterModelDto>();
        }
    }
}