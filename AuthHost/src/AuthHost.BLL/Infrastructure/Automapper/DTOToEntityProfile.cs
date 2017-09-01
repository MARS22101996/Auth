using AuthHost.BLL.DTO;
using AuthHost.DAL.Entities;
using AutoMapper;

namespace AuthHost.BLL.Infrastructure.Automapper
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>();
            CreateMap<RegisterModelDto, User>();
        }
    }
}