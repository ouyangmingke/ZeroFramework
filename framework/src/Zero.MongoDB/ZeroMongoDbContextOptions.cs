using MongoDB.Driver;

namespace DataAcquisition.MongoDB
{
    public class ZeroMongoDbContextOptions
    {
        public Action<MongoClientSettings> MongoClientSettingsConfigurer { get; set; }
    }
}
