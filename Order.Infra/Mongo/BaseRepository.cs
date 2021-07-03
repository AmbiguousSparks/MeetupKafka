using MongoDB.Driver;
using Order.Domain.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Mongo
{
    public abstract class BaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<TEntity> _dbSet;

        public FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;

        protected BaseRepository(IMongoContext context)
        {
            _context = context;
            _dbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        
        public virtual async Task Add(TEntity entity)
        {
            await _dbSet.InsertOneAsync(entity);
        }

        public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var data = await _dbSet.FindAsync(Filter.Eq("_id", id), cancellationToken: cancellationToken);
            return await data.FirstOrDefaultAsync(cancellationToken);
        }
        public virtual async Task<IList<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            var all = await _dbSet.FindAsync(Filter.Empty, cancellationToken: cancellationToken);
            return all.ToList(cancellationToken);
        }

        public virtual async Task Update(TEntity obj, CancellationToken cancellationToken = default)
        {
            await _dbSet.ReplaceOneAsync(Filter.Eq("_id", obj.Id), obj, cancellationToken: cancellationToken);
        }

        public virtual async Task Remove(Guid id)
        {
            await _dbSet.DeleteOneAsync(Filter.Eq("_id", id));
        }
    }
}
