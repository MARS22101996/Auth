using MongoDB.Driver;
using UserService.DAL.Entities;

namespace UserService.DAL.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : BaseType;
    }
}