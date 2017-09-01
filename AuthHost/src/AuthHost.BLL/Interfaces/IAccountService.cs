using System.Threading.Tasks;
using AuthHost.BLL.DTO;

namespace AuthHost.BLL.Interfaces
{
    public interface IAccountService
    {
        UserDto Login(LoginModelDto loginModelDto);

        Task RegisterAsync(RegisterModelDto registerModelDto);
    }
}