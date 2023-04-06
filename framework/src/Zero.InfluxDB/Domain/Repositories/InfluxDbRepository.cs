using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Zero.Ddd.Domain.Entities;
using Zero.Ddd.Domain.Repositories;

namespace Zero.InfluxDB.Domain.Repositories
{
    public class InfluxDbRepository<TInfluxDbContext, TEntity> : IRepository, IInfluxDbRepository<TEntity>
         where TInfluxDbContext : IZeroInfluxDbContext
         where TEntity : class, IEntity
    {
        protected IInfluxDbContextProvider<TInfluxDbContext> DbContextProvider { get; }
        public InfluxDbRepository(IInfluxDbContextProvider<TInfluxDbContext> dbContextProvider)
        {
            DbContextProvider = dbContextProvider;
        }
        protected Task<TInfluxDbContext> GetDbContextAsync(CancellationToken cancellationToken = default)
        {
            return DbContextProvider.GetDbContextAsync(cancellationToken);
        }
        /// <summary>
        /// 获取连接器
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<InfluxDBClient> InfluxDBClientAsync()
        {
            var dbContext = await GetDbContextAsync();
            return dbContext.Client;
        }

        #region 写入数据

        /// <summary>
        /// 创建 写入API
        /// </summary>
        /// <param name="action"></param>
        protected virtual async Task WriteApiAsync(Func<WriteApiAsync, Task> action)
        {
            using var client = await InfluxDBClientAsync();
            var writeApi = client.GetWriteApiAsync();
            await action(writeApi);
        }
        public virtual async Task WriteMeasurementAsync<T>(T value)
        {
            await WriteApiAsync(async (writeApi) =>
            {
                await writeApi.WriteMeasurementAsync(value, WritePrecision.Ns);
            });
        }
        public virtual async Task WriteMeasurementsAsync<T>(List<T> values)
        {
            await WriteApiAsync(async (writeApi) =>
                {
                    await writeApi.WriteMeasurementsAsync(values, WritePrecision.Ns);
                });
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await WriteMeasurementAsync(entity);
            return entity;
        }

        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            await WriteMeasurementsAsync(entitys.ToList());
        }
        #endregion

        #region 查询数据
        public virtual async Task<List<TEntity>> GetListAsync(string flux, CancellationToken cancellationToken = default)
        {
            using var client = await InfluxDBClientAsync();
            var queryApi = client.GetQueryApi();
            return await queryApi.QueryAsync<TEntity>(flux);
        }
        #endregion

        #region 删除数据
        public virtual async Task DeleteAsync(DeletePredicateRequest predicate, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            using var client = dbContext.Client;
            var deleteApi = client.GetDeleteApi();
            await deleteApi.Delete(predicate, dbContext.Bucket, dbContext.Org);
        }
        #endregion
    }
}
