using System.Linq.Expressions;

using Zero.Ddd.Domain.Entities;

namespace Zero.Ddd.Domain.Repositories
{
    public interface IRepository
    {

    }
    public interface IRepository<TEntity> : IRepository
           where TEntity : class, IEntity
    {
        public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task InsertManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
        public Task<IQueryable<TEntity>> GetQueryableAsync();
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task UpdateManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task DeleteManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
         where TEntity : class, IEntity
    {
    }
}