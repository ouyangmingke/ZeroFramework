using MongoDB.Driver;

namespace Zero.MongoDB.Domain
{
    public interface IZeroMongoDbContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }

        IMongoCollection<T> Collection<T>();

        IClientSessionHandle SessionHandle { get; }
    }
}
