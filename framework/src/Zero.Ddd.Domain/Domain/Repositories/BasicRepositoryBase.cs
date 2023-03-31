using System.Linq.Expressions;

using Zero.Ddd.Domain.Entities;
using Zero.Ddd.Domain.Repositories;

namespace Zero.Ddd.Domain.Domain.Repositories
{
    public abstract class BasicRepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public abstract Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        public virtual async Task DeleteManyAsyncAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entitys)
            {
                await DeleteAsync(entity, cancellationToken);
            }
        }

        public abstract Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        public abstract Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        public abstract Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var item in entitys)
            {
                await InsertAsync(item, cancellationToken);
            }
        }

        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var item in entitys)
            {
                await UpdateAsync(item, cancellationToken);
            }
        }
    }
}