using AuthHost.DAL.Entities;

namespace AuthHost.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }

        IRepository<Role> Roles { get; }
    }
}