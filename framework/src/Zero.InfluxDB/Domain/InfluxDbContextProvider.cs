using InfluxDB.Client;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

using Zero.Data.Domain;

namespace Zero.InfluxDB.Domain
{
    public class InfluxDbContextProvider<TInfluxDbContext> : IInfluxDbContextProvider<TInfluxDbContext>
        where TInfluxDbContext : ZeroInfluxDbContext
    {
        public InfluxDbContextProvider(
            IConnectionStringResolver connectionStringResolver,
            IOptions<ZeroInfluxDbContextOptions> options,
            IServiceProvider serviceProvider)
        {
            Options = options.Value;
            ConnectionStringResolver = connectionStringResolver;
            ServiceProvider = serviceProvider;
        }

        public readonly ZeroInfluxDbContextOptions Options;
        private readonly IConnectionStringResolver ConnectionStringResolver;
        public readonly IServiceProvider ServiceProvider;
        public async Task<TInfluxDbContext> GetDbContextAsync(CancellationToken cancellationToken = default)
        {
            var targetDbContextType = typeof(TInfluxDbContext);
            var connectionString = await ResolveConnectionStringAsync(targetDbContextType);
            var options = new InfluxDBClientOptions(connectionString);
            var bucketName = options.Bucket;
            if (string.IsNullOrWhiteSpace(bucketName))
            {
                options.Bucket = ConnectionStringNameAttribute.GetConnStringName(targetDbContextType);
            }
            var org = options.Org;
            if (string.IsNullOrWhiteSpace(org))
            {
                options.Org = InfluxOrganizationAttribute.GetOrganization(targetDbContextType);
            }
            return CreateDbContext(options);
        }
        protected virtual async Task<string> ResolveConnectionStringAsync(Type dbContextType)
        {
            return await ConnectionStringResolver.ResolveAsync(dbContextType);
        }
        protected virtual TInfluxDbContext CreateDbContext(
            InfluxDBClientOptions options,
            CancellationToken cancellationToken = default)
        {
            var client = CreateInfluxDBClient(options);
            var dbContext = ServiceProvider.GetRequiredService<TInfluxDbContext>();
            dbContext.InitializeDatabase(client, options);
            return dbContext;
        }
        protected virtual InfluxDBClient CreateInfluxDBClient(InfluxDBClientOptions options)
        {
            Options.InfluxDBClientOptionsConfigurer?.Invoke(options);
            return new InfluxDBClient(options);
        }
    }
}
