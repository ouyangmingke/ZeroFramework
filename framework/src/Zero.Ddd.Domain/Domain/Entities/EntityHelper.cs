﻿using JetBrains.Annotations;

using System.Reflection;

namespace Zero.Ddd.Domain.Entities
{
    public static class EntityHelper
    {
        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        [CanBeNull]
        public static Type FindPrimaryKeyType<TEntity>()
            where TEntity : IEntity
        {
            return FindPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        [CanBeNull]
        public static Type FindPrimaryKeyType([NotNull] Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception(
                    $"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity).AssemblyQualifiedName}!");
            }

            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }

    }
}
