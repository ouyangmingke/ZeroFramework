using MongoDB.Driver;

namespace Zero.MongoDB
{
    public class ZeroMongoDbContextOptions
    {
        public Action<MongoClientSettings> MongoClientSettingsConfigurer { get; set; }
    }
}
