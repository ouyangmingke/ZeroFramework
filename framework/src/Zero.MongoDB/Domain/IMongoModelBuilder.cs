using JetBrains.Annotations;

namespace Zero.MongoDB.Domain
{
    public interface IMongoModelBuilder
    {
        void Entity<TEntity>(Action<IMongoEntityModelBuilder<TEntity>> buildAction = null);

        void Entity([NotNull] Type entityType, Action<IMongoEntityModelBuilder> buildAction = null);

        IReadOnlyList<IMongoEntityModelBuilder> GetEntities();
    }
}
