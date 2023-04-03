using JetBrains.Annotations;

using System.Collections.Immutable;

namespace Zero.MongoDB.Domain
{
    public class MongoModelBuilder : IMongoModelBuilder
    {
        private readonly Dictionary<Type, IMongoEntityModelBuilder> _entityModelBuilders;
        private static readonly object SyncObj = new object();

        public MongoModelBuilder()
        {
            _entityModelBuilders = new Dictionary<Type, IMongoEntityModelBuilder>();
        }

        public IReadOnlyList<IMongoEntityModelBuilder> GetEntities()
        {
            throw new NotImplementedException();
        }
        public virtual void Entity<TEntity>(Action<IMongoEntityModelBuilder<TEntity>> buildAction = null)
        {
            var model = (IMongoEntityModelBuilder<TEntity>)_entityModelBuilders.GetOrAdd(
                typeof(TEntity),
                () => new MongoEntityModelBuilder<TEntity>()
            );

            buildAction?.Invoke(model);
        }
        public void Entity([NotNull] Type entityType, Action<IMongoEntityModelBuilder> buildAction = null)
        {
            var model = _entityModelBuilders.GetOrAdd(
                   entityType, () => (IMongoEntityModelBuilder)Activator.CreateInstance(
                       typeof(MongoEntityModelBuilder<>).MakeGenericType(entityType)
                       )
                   );
            buildAction?.Invoke(model);
        }

        public virtual MongoDbContextModel Build(ZeroMongoDbContext dbContext)
        {
            var entityModels = _entityModelBuilders
                .Select(x => x.Value)
                .Cast<IMongoEntityModel>()
                .ToImmutableDictionary(x => x.EntityType, x => x);
            return new MongoDbContextModel(entityModels);
        }
    }
}
