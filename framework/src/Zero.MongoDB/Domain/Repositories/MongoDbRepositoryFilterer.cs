using MongoDB.Driver;

using Zero.Core;
using Zero.Data;
using Zero.Ddd.Domain.Entities;

namespace Zero.MongoDB.Domain.Repositories;

public class MongoDbRepositoryFilterer<TEntity> : IMongoDbRepositoryFilterer<TEntity>
    where TEntity : class, IEntity
{
    protected IDataFilter DataFilter { get; }
    public MongoDbRepositoryFilterer(IDataFilter dataFilter)
    {
        DataFilter = dataFilter;
    }

    public virtual Task AddGlobalFiltersAsync(List<FilterDefinition<TEntity>> filters)
    {
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && DataFilter.IsEnabled<ISoftDelete>())
        {
            filters.Add(Builders<TEntity>.Filter.Eq(e => ((ISoftDelete)e).IsDeleted, false));
        }

        return Task.CompletedTask;
    }
}

public class MongoDbRepositoryFilterer<TEntity, TKey> : MongoDbRepositoryFilterer<TEntity>,
    IMongoDbRepositoryFilterer<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{

    public MongoDbRepositoryFilterer(IDataFilter dataFilter)
        : base(dataFilter)
    {
    }

    public virtual async Task<FilterDefinition<TEntity>> CreateEntityFilterAsync(TKey id, bool applyFilters = false)
    {
        var filters = new List<FilterDefinition<TEntity>>
            {
                Builders<TEntity>.Filter.Eq(e => e.Id, id)
            };

        if (applyFilters)
        {
            await AddGlobalFiltersAsync(filters);
        }

        return Builders<TEntity>.Filter.And(filters);
    }

    public virtual Task<FilterDefinition<TEntity>> CreateEntityFilterAsync(TEntity entity, bool withConcurrencyStamp = false, string concurrencyStamp = null)
    {
        if (!withConcurrencyStamp)
        {
            return Task.FromResult(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id));
        }

        return Task.FromResult(Builders<TEntity>.Filter.And(
            Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id)
        ));
    }

    public virtual async Task<FilterDefinition<TEntity>> CreateEntitiesFilterAsync(IEnumerable<TEntity> entities, bool applyFilters = false)
    {
        return await CreateEntitiesFilterAsync(entities.Select(s => s.Id), applyFilters);
    }

    public virtual async Task<FilterDefinition<TEntity>> CreateEntitiesFilterAsync(IEnumerable<TKey> ids, bool applyFilters = false)
    {
        var filters = new List<FilterDefinition<TEntity>>()
            {
                Builders<TEntity>.Filter.In(e => e.Id, ids),
            };

        if (applyFilters)
        {
            await AddGlobalFiltersAsync(filters);
        }

        return Builders<TEntity>.Filter.And(filters);
    }
}
