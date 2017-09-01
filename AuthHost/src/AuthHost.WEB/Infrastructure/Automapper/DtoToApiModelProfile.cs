using AuthHost.BLL.DTO;
using AuthHost.WEB.Models.AccountApiModels;
using AutoMapper;

namespace AuthHost.WEB.Infrastructure.Automapper
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