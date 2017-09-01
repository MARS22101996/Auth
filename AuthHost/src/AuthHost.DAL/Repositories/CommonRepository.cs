using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthHost.DAL.Entities;
using AuthHost.DAL.Interfaces;
using MongoDB.Driver;

namespace AuthHost.DAL.Repositories
{
    public class CommonRepository<TEntity> : IRepository<TEntity> where TEntity : BaseType, new()
    {
        protected readonly IDbContext Context;
        protected readonly string CollectionName;

        public CommonRepository(IDbContext context)
        {
            Context = context;
            CollectionName = new TEntity().CollectionName;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);

            return collection.AsQueryable();
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);
            var entity = (await collection.FindAsync(e => e.Id.Equals(id))).FirstOrDefault();

            return entity;
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);
            var filterCompile = expression.Compile();
            var entities = collection.AsQueryable().Where(filterCompile);

            return entities.AsQueryable();
        }

        public virtual IQueryable<TEntity> Find(int skip, int take, Expression<Func<TEntity, bool>> expression = null)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);
            IQueryable<TEntity> entities = collection.AsQueryable();

            if (expression != null)
            {
                var filterCompile = expression.Compile();
                entities = entities.Where(filterCompile).AsQueryable();
            }

            entities = entities.Skip(skip).Take(take);

            return entities;
        }

        public virtual async Task CreateAsync(TEntity item)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);
            item.Id = Guid.NewGuid();

            await collection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(TEntity item)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);

            await collection.ReplaceOneAsync(entity => entity.Id.Equals(item.Id), item);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var collection = Context.GetCollection<TEntity>(CollectionName);

            await collection.DeleteOneAsync(entity => entity.Id.Equals(id));
        }
    }
}