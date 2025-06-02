using MonopolyStorage.DataAccess.Entities.Base;

namespace MonopolyStorage.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<TKey, TEntity> where TEntity : EntityBase<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<string> includeProperties = null);

        Task<TKey> AddAsync(TEntity entity);

        Task<TKey> UpdateAsync(TEntity entity);

        Task<TKey> Delete(TEntity entity);
    }
}
