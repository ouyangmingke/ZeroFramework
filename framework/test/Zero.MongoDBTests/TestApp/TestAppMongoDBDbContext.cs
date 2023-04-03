using MongoDB.Driver;

using Zero.Data.Domain;
using Zero.MongoDB.Domain;
using Zero.TestApp.Domain.Data;

namespace Zero.MongoDBTests.TestApp
{
    [ConnectionStringName("Zero")]
    public class TestAppMongoDBDbContext : ZeroMongoDbContext
    {
        [MongoCollection("Restaurant")]
        public IMongoCollection<Restaurant> Restaurant => Collection<Restaurant>();
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);
        }
    }
}