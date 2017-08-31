using System.Threading.Tasks;
using UserService.BLL.DTO;

namespace UserService.BLL.Interfaces
{
	public interface IRoleService
    {
        RoleDto Get(string name);

        Task CreateAsync(string name);
    }
}