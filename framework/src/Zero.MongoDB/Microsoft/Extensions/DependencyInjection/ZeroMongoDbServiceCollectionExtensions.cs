using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Zero.MongoDB.Domain;
using Zero.MongoDB.Domain.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZeroMongoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, Action<IZeroMongoDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
            where TMongoDbContext : ZeroMongoDbContext
        {
            services.AddTransient<TMongoDbContext>();
            var options = new ZeroMongoDbContextRegistrationOptions(typeof(TMongoDbContext), services);
            optionsBuilder?.Invoke(options);
            new MongoDbRepositoryRegistrar(options).AddRepositories();
            return services;
        }
    }
}