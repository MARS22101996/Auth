using AutoMapper;
using UserService.BLL.DTO;
using UserService.DAL.Entities;

namespace UserService.BLL.Infrastructure.Automapper
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