using InfluxDB.Client.Api.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.InfluxDB.Domain.Repositories
{
    public interface IInfluxDbRepository<TEntity>
    {
        public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task InsertManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
        public Task<List<TEntity>> GetListAsync(string flux, CancellationToken cancellationToken = default);
        public Task DeleteAsync(DeletePredicateRequest predicate, CancellationToken cancellationToken = default);
    }
}
