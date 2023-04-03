namespace Zero.MongoDB.Domain;

public interface IMongoModelSource
{
    MongoDbContextModel GetModel(ZeroMongoDbContext dbContext);
}
