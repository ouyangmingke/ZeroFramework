using DataAcquisition.MongoDB;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using Zero.Data.Domain;

namespace Zero.MongoDB.Domain
{
    public class MongoDbContextProvider<TMongoDbContext> : IMongoDbContextProvider<TMongoDbContext>
          where TMongoDbContext : ZeroMongoDbContext
    {
        public MongoDbContextProvider(
            IConnectionStringResolver connectionStringResolver,
            IOptions<ZeroMongoDbContextOptions> options,
            IServiceProvider serviceProvider)
        {
            Options = options.Value;
            ConnectionStringResolver = connectionStringResolver;
            ServiceProvider = serviceProvider;
        }

        public readonly ZeroMongoDbContextOptions Options;
        private readonly IConnectionStringResolver ConnectionStringResolver;
        public readonly IServiceProvider ServiceProvider;

        public async Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default)
        {
            var targetDbContextType = typeof(TMongoDbContext);

            var connectionString = await ResolveConnectionStringAsync(targetDbContextType);
            //var dbContextKey = $"{targetDbContextType.FullName}_{connectionString}";
          
            var mongoUrl = new MongoUrl(connectionString);
            var databaseName = mongoUrl.DatabaseName;
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                databaseName = ConnectionStringNameAttribute.GetConnStringName(targetDbContextType);
            }
            return CreateDbContext(mongoUrl, databaseName);

        }
        
        protected virtual MongoClient CreateMongoClient(MongoUrl mongoUrl)
        {
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);
            Options.MongoClientSettingsConfigurer?.Invoke(mongoClientSettings);
            return new MongoClient(mongoClientSettings);
        }
        /// <summary>
        /// 解析连接字符串
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <returns></returns>
        protected virtual async Task<string> ResolveConnectionStringAsync(Type dbContextType)
        {
            return await ConnectionStringResolver.ResolveAsync(dbContextType);
        }

        /// <summary>
        /// 创建 DBContext
        /// </summary>
        /// <param name="mongoUrl"></param>
        /// <param name="databaseName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual TMongoDbContext CreateDbContext(
            MongoUrl mongoUrl,
            string databaseName,
            CancellationToken cancellationToken = default)
        {
            var client = CreateMongoClient(mongoUrl);
            var database = client.GetDatabase(databaseName);
            var dbContext = ServiceProvider.GetRequiredService<TMongoDbContext>();
            var modelSource = ServiceProvider.GetRequiredService<IMongoModelSource>();
            dbContext.InitializeDatabase(modelSource,database, client, null);
            return dbContext;
        }
    }
}
