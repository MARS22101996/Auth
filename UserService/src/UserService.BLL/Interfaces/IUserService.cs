using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.BLL.DTO;

namespace UserService.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        Task<UserDto> GetAsync(Guid id);

        UserDto Get(string email);

        Task AddToRoleAsync(Guid userId, string role);

        Task RemoveRoleAsync(Guid userId, string role);

        Task<bool> IsInRoleAsync(Guid userId, string role);

        IEnumerable<string> GetEmailsForSearch(string term);
    }
}