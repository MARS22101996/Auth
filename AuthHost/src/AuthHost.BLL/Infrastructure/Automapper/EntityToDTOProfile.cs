using AuthHost.BLL.DTO;
using AuthHost.DAL.Entities;
using AutoMapper;

namespace AuthHost.BLL.Infrastructure.Automapper
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>();
        }
    }
}