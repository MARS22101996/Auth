using System.Threading.Tasks;
using AuthHost.BLL.DTO;

namespace AuthHost.BLL.Interfaces
{
	public interface IRoleService
    {
        RoleDto Get(string name);

        Task CreateAsync(string name);
    }
}