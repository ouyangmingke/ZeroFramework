using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq.Expressions;

using Zero.Core;
using Zero.Ddd.Domain.Entities;
using Zero.Ddd.Domain.Repositories;

namespace Zero.MongoDB.Domain.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMongoDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoDbRepository<TMongoDbContext, TEntity> : BasicRepositoryBase<TEntity>
         where TMongoDbContext : IZeroMongoDbContext
         where TEntity : class, IEntity
    {
        protected IMongoDbContextProvider<TMongoDbContext> DbContextProvider { get; }
        public MongoDbRepository(IMongoDbContextProvider<TMongoDbContext> dbContextProvider)
        {
            DbContextProvider = dbContextProvider;
        }

        protected Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default)
        {
            return DbContextProvider.GetDbContextAsync(cancellationToken);
        }
        public async Task<IMongoCollection<TEntity>> GetCollectionAsync(CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync(cancellationToken);
            return dbContext.Collection<TEntity>();
        }
        public virtual async Task<IMongoQueryable<TEntity>> GetMongoQueryableAsync(CancellationToken cancellationToken = default, AggregateOptions aggregateOptions = null)
        {
            var dbContext = await GetDbContextAsync(cancellationToken);
            var collection = dbContext.Collection<TEntity>();

            return dbContext.SessionHandle != null
                    ? collection.AsQueryable(dbContext.SessionHandle, aggregateOptions)
                    : collection.AsQueryable(aggregateOptions);

        }
        public override async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await GetMongoQueryableAsync();
        }
        public override async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync(cancellationToken);
            var collection = dbContext.Collection<TEntity>();

            if (dbContext.SessionHandle != null)
            {
                await collection.InsertOneAsync(
                    dbContext.SessionHandle,
                entity,
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                await collection.InsertOneAsync(
                entity,
                    cancellationToken: cancellationToken
                );
            }

            return entity;
        }
        public override async Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return (await GetMongoQueryableAsync()).Where(predicate).SingleOrDefault();
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);

            var documents = await collection.FindAsync(predicate);
            return await documents.ToListAsync();

        }

        public override async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync(cancellationToken);
            var collection = dbContext.Collection<TEntity>();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                ReplaceOneResult result;
                if (dbContext.SessionHandle != null)
                {
                    result = await collection.ReplaceOneAsync(
                        dbContext.SessionHandle,
                        await CreateEntityFilterAsync(entity, true),
                        entity,
                        cancellationToken: cancellationToken
                    );
                }
                else
                {
                    result = await collection.ReplaceOneAsync(
                        await CreateEntityFilterAsync(entity, true),
                        entity,
                        cancellationToken: cancellationToken
                    );
                }
                if (result.MatchedCount <= 0)
                {
                    ThrowOptimisticConcurrencyException();
                }
            }
            else
            {
                DeleteResult result;
                if (dbContext.SessionHandle != null)
                {
                    result = await collection.DeleteOneAsync(
                        dbContext.SessionHandle,
                        await CreateEntityFilterAsync(entity, true),
                        cancellationToken: cancellationToken
                    );
                }
                else
                {
                    result = await collection.DeleteOneAsync(
                        await CreateEntityFilterAsync(entity, true),
                        cancellationToken
                    );
                }

                if (result.DeletedCount <= 0)
                {
                    ThrowOptimisticConcurrencyException();
                }
            }
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync(cancellationToken);
            ReplaceOneResult result;
            var collection = dbContext.Collection<TEntity>();

            if (dbContext.SessionHandle != null)
            {
                result = await collection.ReplaceOneAsync(
                    dbContext.SessionHandle,
                    await CreateEntityFilterAsync(entity, true),
                    entity,
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                result = await collection.ReplaceOneAsync(
                    await CreateEntityFilterAsync(entity, true),
                    entity,
                    cancellationToken: cancellationToken
                );
            }

            if (result.MatchedCount <= 0)
            {
                ThrowOptimisticConcurrencyException();
            }

            return entity;

        }

        protected virtual Task<FilterDefinition<TEntity>> CreateEntityFilterAsync(TEntity entity, bool withConcurrencyStamp = false, string concurrencyStamp = null)
        {
            throw new NotImplementedException(
                $"{nameof(CreateEntityFilterAsync)} is not implemented for MongoDB by default. It should be overriden and implemented by the deriving class!"
            );
        }

        protected virtual Task<FilterDefinition<TEntity>> CreateEntitiesFilterAsync(IEnumerable<TEntity> entities, bool withConcurrencyStamp = false)
        {
            throw new NotImplementedException(
              $"{nameof(CreateEntitiesFilterAsync)} is not implemented for MongoDB by default. It should be overriden and implemented by the deriving class!"
          );
        }

        protected virtual void ThrowOptimisticConcurrencyException()
        {
            throw new Exception("Database operation expected to affect 1 row but actually affected 0 row. Data may have been modified or deleted since entities were loaded. This exception has been thrown on optimistic concurrency check.");
        }

    }

    public class MongoDbRepository<TMongoDbContext, TEntity, TKey> : MongoDbRepository<TMongoDbContext, TEntity>
         where TMongoDbContext : IZeroMongoDbContext
         where TEntity : class, IEntity<TKey>
    {
        public IMongoDbRepositoryFilterer<TEntity, TKey> RepositoryFilterer { get; set; }

        public MongoDbRepository(IMongoDbContextProvider<TMongoDbContext> dbContextProvider
            , IMongoDbRepositoryFilterer<TEntity, TKey> repositoryFilterer
            ) : base(dbContextProvider)
        {
            RepositoryFilterer = repositoryFilterer;
        }

        protected async override Task<FilterDefinition<TEntity>> CreateEntityFilterAsync(TEntity entity, bool withConcurrencyStamp = false, string concurrencyStamp = null)
        {
            return await RepositoryFilterer.CreateEntityFilterAsync(entity, withConcurrencyStamp, concurrencyStamp);
        }

        protected async override Task<FilterDefinition<TEntity>> CreateEntitiesFilterAsync(IEnumerable<TEntity> entities, bool withConcurrencyStamp = false)
        {
            return await RepositoryFilterer.CreateEntitiesFilterAsync(entities, withConcurrencyStamp);
        }
    }
}
