namespace Zero.MongoDB.Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MongoCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }
        public MongoCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
