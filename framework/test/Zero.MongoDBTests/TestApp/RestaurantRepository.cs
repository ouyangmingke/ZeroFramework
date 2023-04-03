using MongoDB.Driver;

using System.Linq.Expressions;

using Zero.Core;
using Zero.MongoDB.Domain;
using Zero.MongoDB.Domain.Repositories;
using Zero.TestApp.Domain.Data;

namespace Zero.MongoDBTests.TestApp
{
    public class RestaurantRepository : MongoDbRepository<TestAppMongoDBDbContext, Restaurant>
    {
        public RestaurantRepository(IMongoDbContextProvider<TestAppMongoDBDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<List<Restaurant>> GetListAsync(Expression<Func<Restaurant, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"MongoDbTestRepository");
            return await base.GetListAsync(predicate, cancellationToken);
        }
        protected override Task<FilterDefinition<Restaurant>> CreateEntityFilterAsync(Restaurant entity, bool withConcurrencyStamp = false, string concurrencyStamp = null)
        {
            return Task.FromResult(Builders<Restaurant>.Filter.Eq(e => e.Id, entity.Id));
        }
    }
}
