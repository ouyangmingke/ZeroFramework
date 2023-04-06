using System.Linq.Expressions;

using Zero.Ddd.Domain.Entities;

namespace Zero.Ddd.Domain.Repositories
{
    public abstract class BasicRepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public abstract Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var item in entitys)
            {
                await InsertAsync(item, cancellationToken);
            }
        }
        public abstract Task<IQueryable<TEntity>> GetQueryableAsync();
        public abstract Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(predicate, cancellationToken);
            if (entity == null)
            {
                throw new Exception(typeof(TEntity).FullName);
            }
            return entity;
        }

        public abstract Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var item in entitys)
            {
                await UpdateAsync(item, cancellationToken);
            }
        }
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = await GetListAsync(predicate);
            await DeleteManyAsync(entities);
        }

        public abstract Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entitys)
            {
                await DeleteAsync(entity, cancellationToken);
            }
        }
    }
}