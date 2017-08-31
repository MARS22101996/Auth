using System.Threading.Tasks;
using UserService.BLL.DTO;

namespace UserService.BLL.Interfaces
{
    public interface IAccountService
    {
        UserDto Login(LoginModelDto loginModelDto);

        Task RegisterAsync(RegisterModelDto registerModelDto);
    }
}