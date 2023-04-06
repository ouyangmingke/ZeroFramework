using Microsoft.Extensions.DependencyInjection;

using Zero.Core.Modularity;
using Zero.Data.Domain;
using Zero.MongoDB;
using Zero.MongoDBTests.TestApp;
using Zero.TestApp.Domain.Data;

namespace Zero.MongoDBTests
{
    [DependsOn(typeof(ZeroMongoDbModule))]
    public class MongoDbTestModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddMongoDbContext<TestAppMongoDBDbContext>(option =>
            {
                option.AddDefaultRepositories();
                //option.AddRepository<Restaurant, RestaurantRepository>();
            });
            services.Configure<ZeroDbConnectionOptions>(t =>
            t.ConnectionStrings.Default = "mongodb://127.0.0.1:27017/"
            );
            return base.ConfigureServicesAsync(services);
        }
    }
}
