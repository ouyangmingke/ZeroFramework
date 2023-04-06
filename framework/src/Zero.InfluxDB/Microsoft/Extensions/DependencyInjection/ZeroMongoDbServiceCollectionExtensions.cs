using System;

using Zero.InfluxDB.Domain;
using Zero.InfluxDB.Domain.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZeroInfluxDbServiceCollectionExtensions
    {
        public static IServiceCollection AddInfluxDbContext<TInfluxDbContext>(this IServiceCollection services, Action<IZeroInfluxDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
            where TInfluxDbContext : ZeroInfluxDbContext
        {
            services.AddTransient<TInfluxDbContext>();
            var options = new ZeroInfluxDbContextRegistrationOptions(typeof(TInfluxDbContext), services);
            optionsBuilder?.Invoke(options);
            new InfluxDbRepositoryRegistrar(options).AddRepositories();
            return services;
        }
     
    }
}