using MongoDB.Bson.Serialization;

namespace Zero.MongoDB.Domain
{
    public interface IMongoEntityModelBuilder<TEntity>
    {
        Type EntityType { get; }

        string CollectionName { get; set; }
    }

    public interface IMongoEntityModelBuilder
    {
        Type EntityType { get; }

        string CollectionName { get; set; }

    }
}
