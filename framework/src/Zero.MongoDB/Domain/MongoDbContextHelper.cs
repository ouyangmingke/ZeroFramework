using MongoDB.Driver;

using System.Reflection;

using Zero.Core.Reflection;
using Zero.Ddd.Domain.Entities;

namespace Zero.MongoDB.Domain
{
    internal static class MongoDbContextHelper
    {
        public static IEnumerable<PropertyInfo> GetEntityPropertyInfo(Type dbContextType)
        {
            return
                from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IMongoCollection<>)) &&
                    typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
                select property;
        }
        public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return
                GetEntityPropertyInfo(dbContextType).
                Select(t => t.PropertyType.GenericTypeArguments[0]);
        }
    }
}
