using AuthHost.BLL.DTO;
using AuthHost.WEB.Models.AccountApiModels;
using AutoMapper;

namespace AuthHost.WEB.Infrastructure.Automapper
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