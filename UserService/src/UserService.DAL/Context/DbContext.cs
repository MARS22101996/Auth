using MongoDB.Driver;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.DAL.Context
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database;

        public DbContext()
        {
        }

        public DbContext(string connectionString)
        {
            var builder = new MongoUrlBuilder(connectionString);

            IMongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(builder.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
            where TEntity : BaseType
        {
            return _database.GetCollection<TEntity>(collectionName);
        }
    }
}