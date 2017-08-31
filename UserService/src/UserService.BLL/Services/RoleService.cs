using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTO;
using UserService.BLL.Infrastructure.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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

            _logger.LogInformation($"Get role by name: {name}");

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

            _logger.LogInformation($"New role {name} created");
        }
    }
}