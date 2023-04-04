using MongoDB.Driver;

using System.Collections.Concurrent;
using System.Reflection;

using Zero.Core.DependencyInjection;

namespace Zero.MongoDB.Domain;

/// <summary>
/// Model �ṩ��
/// </summary>
public class MongoModelSource : IMongoModelSource, ISingletonDependency
{
    protected readonly ConcurrentDictionary<Type, MongoDbContextModel> ModelCache = new();

    /// <summary>
    /// ��ȡ Model
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    public virtual MongoDbContextModel GetModel(ZeroMongoDbContext dbContext)
    {
        return ModelCache.GetOrAdd(
            dbContext.GetType(),
            _ => CreateModel(dbContext)
        );
    }

    protected virtual MongoDbContextModel CreateModel(ZeroMongoDbContext dbContext)
    {
        var modelBuilder = CreateModelBuilder();
        BuildModelFromDbContextType(modelBuilder, dbContext.GetType());
        BuildModelFromDbContextInstance(modelBuilder, dbContext);
        return modelBuilder.Build(dbContext);
    }

    protected virtual MongoModelBuilder CreateModelBuilder()
    {
        return new MongoModelBuilder();
    }

    /// <summary>
    /// ��ȡ DBcontext�� ����Ϊ IMongoCollection��T�� ������
    /// T �̳��� IEntity
    /// </summary>
    /// <param name="dbContextType"></param>
    protected virtual void BuildModelFromDbContextType(MongoModelBuilder modelBuilder, Type dbContextType)
    {
        var collectionProperties = MongoDbContextHelper.GetEntityPropertyInfo(dbContextType);

        foreach (var collectionProperty in collectionProperties)
        {
            BuildModelFromDbContextCollectionProperty(modelBuilder, collectionProperty);
        }
    }

    /// <summary>
    /// ��ȡ Model CollectionName
    /// </summary>
    /// <param name="collectionProperty"></param>
    protected virtual void BuildModelFromDbContextCollectionProperty(MongoModelBuilder modelBuilder, PropertyInfo collectionProperty)
    {
        var entityType = collectionProperty.PropertyType.GenericTypeArguments[0];
        var collectionAttribute = collectionProperty.GetCustomAttributes().OfType<MongoCollectionAttribute>().FirstOrDefault();
        modelBuilder.Entity(entityType, b =>
        {
            b.CollectionName = collectionAttribute?.CollectionName ?? collectionProperty.Name;
        });
    }

    protected virtual void BuildModelFromDbContextInstance(IMongoModelBuilder modelBuilder, ZeroMongoDbContext dbContext)
    {
        dbContext.CreateModel(modelBuilder);
    }
}
