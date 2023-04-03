namespace Zero.MongoDB.Domain
{
    public interface IMongoEntityModel
    {
        Type EntityType { get; }

        string CollectionName { get; }
    }
}
