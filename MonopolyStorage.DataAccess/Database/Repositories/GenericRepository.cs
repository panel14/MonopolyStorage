using Microsoft.EntityFrameworkCore;
using MonopolyStorage.DataAccess.Entities.Base;
using MonopolyStorage.Domain.Repositories.Interfaces;

namespace MonopolyStorage.DataAccess.Database.Repositories
{
    public abstract class GenericRepository<TKey, TEntity>(DbContext context)
        : IGenericRepository<TKey, TEntity> where TEntity
        : EntityBase<TKey> where TKey : notnull
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<TKey> AddAsync(TEntity entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity.Id;
        }

        public async Task<TKey> Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<string> includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToArrayAsync();
        }

        public async Task<TKey> UpdateAsync(TEntity entity)
        {
            var result = _dbSet.Update(entity);
            return result.Entity.Id;
        }
    }
}
