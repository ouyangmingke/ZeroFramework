using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Zero.Core.Modularity;
using Zero.Data;
using Zero.Ddd.Domain;
using Zero.MongoDB.Domain;
using Zero.MongoDB.Domain.Repositories;

namespace Zero.MongoDB
{
    [DependsOn(
        typeof(ZeroDataModule),
        typeof(ZeroDddDomainModule))]
    public class ZeroMongoDbModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.TryAddTransient(
                typeof(IMongoDbContextProvider<>),
                typeof(MongoDbContextProvider<>)
                );
            services.TryAddTransient(
                typeof(IMongoDbRepositoryFilterer<,>),
                typeof(MongoDbRepositoryFilterer<,>)
                );
            services.AddSingleton<IMongoModelSource, MongoModelSource>();
            return base.ConfigureServicesAsync(services);
        }
    }
}
