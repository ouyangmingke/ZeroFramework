using Zero.MongoDB.Domain.Repositories;

namespace Zero.MongoDB.Domain.DependencyInjection
{
    public class MongoDbRepositoryRegistrar : RepositoryRegistrarBase<ZeroMongoDbContextRegistrationOptions>
    {
        public MongoDbRepositoryRegistrar(ZeroMongoDbContextRegistrationOptions options) : base(options)
        {
        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return MongoDbContextHelper.GetEntityTypes(dbContextType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(MongoDbRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            return typeof(MongoDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
        }
    }
}
