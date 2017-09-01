using System.Linq;
using System.Threading.Tasks;
using AuthHost.BLL.DTO;
using AuthHost.BLL.Infrastructure.Exceptions;
using AuthHost.BLL.Interfaces;
using AuthHost.DAL.Entities;
using AuthHost.DAL.Interfaces;
using AutoMapper;

namespace AuthHost.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public RoleDto Get(string name)
        {
            var role = _unitOfWork.Roles
                .Find(r => r.Name.Equals(name))
                .FirstOrDefault();

            if (role == null)
            {
                throw new EntityNotFoundException(
                    $"Role with such name does not exist. Name: {name}",
                    "Role");
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return roleDto;
        }

        public async Task CreateAsync(string name)
        {
            var role = _unitOfWork.Roles
                .Find(r => r.Name.Equals(name))
                .FirstOrDefault();

            if (role != null)
            {
                throw new EntityExistsException(
                    $"Role with such name already exists. Name: {name}",
                    "Role");
            }

            var roleToCreate = new Role { Name = name };

            await _unitOfWork.Roles.CreateAsync(roleToCreate);
        }
    }
}