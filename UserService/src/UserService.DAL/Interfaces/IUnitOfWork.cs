using UserService.DAL.Entities;

namespace UserService.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }

        IRepository<Role> Roles { get; }
    }
}