using AuthHost.DAL.Entities;
using MongoDB.Driver;

namespace AuthHost.DAL.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : BaseType;
    }
}