namespace Zero.MongoDB.Domain
{

    public class MongoDbContextModel
    {

        public IReadOnlyDictionary<Type, IMongoEntityModel> Entities { get; }

        public MongoDbContextModel(IReadOnlyDictionary<Type, IMongoEntityModel> entities)
        {
            Entities = entities;
        }
    }
}
