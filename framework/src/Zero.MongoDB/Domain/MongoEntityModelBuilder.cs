namespace Zero.MongoDB.Domain;

public class MongoEntityModelBuilder<TEntity> :
    IMongoEntityModel,
    IMongoEntityModelBuilder,
    IMongoEntityModelBuilder<TEntity>
{
    public Type EntityType { get; }

    public string CollectionName { get; set; }

    public MongoEntityModelBuilder()
    {
        EntityType = typeof(TEntity);
    }

}
