using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTO;
using UserService.BLL.Infrastructure.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DAL.Interfaces;

namespace UserService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IRoleService roleService, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var userDtos = _mapper.Map<List<UserDto>>(users).ToList();

            _logger.LogInformation("Get all users");

            return userDtos;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"User with such id does not exist. Id: {id}", 
                    "User");
            }

            var userDto = _mapper.Map<UserDto>(user);

            _logger.LogInformation($"Get user by id: {id}");

            return userDto;
        }

        public UserDto Get(string email)
        {
            var user = _unitOfWork.Users
                .Find(u => u.Email.Equals(email))
                .FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"User with such username doesn't exist. UserName: {email}", 
                    "User");
            }

            var userDto = _mapper.Map<UserDto>(user);

            _logger.LogInformation($"Get user by email: {email}");

            return userDto;
        }

        public async Task AddToRoleAsync(Guid userId, string role)
        {
            var user = await _unitOfWork.Users.GetAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"User with such id does not exist. Id: {userId}",
                    "User");
            }

            var roleDto = _roleService.Get(role);

            user.Roles = user.Roles.Append(roleDto.Name);

            await _unitOfWork.Users.UpdateAsync(user);

            _logger.LogInformation($"Added role {role} to user with id: {userId}");
        }

        public async Task RemoveRoleAsync(Guid userId, string role)
        {
            var user = await _unitOfWork.Users.GetAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"User with such id does not exist. Id: {userId}",
                    "User");
            }

            if (!user.Roles.Contains(role))
            {
                throw new EntityNotFoundException(
                    $"User isn't in specified role. Role: {role}",
                    "Role");
            }

            user.Roles = user.Roles.Where(roleName => !roleName.Equals(role));

            await _unitOfWork.Users.UpdateAsync(user);

            _logger.LogInformation($"Removed role {role} from user with id: {userId}");
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = await _unitOfWork.Users.GetAsync(userId);

            _logger.LogInformation($"Verified role: {role} in user with id: {userId}");

            return user.Roles.Contains(role);
        }

        public IEnumerable<string> GetEmailsForSearch(string term)
        {
            var users = _unitOfWork.Users.GetAll();

            var emails = users
                .Where(a => a.Email.ToLower().Contains(term.ToLower()))
                .Select(a => a.Email);

            return emails;
        }
    }
}