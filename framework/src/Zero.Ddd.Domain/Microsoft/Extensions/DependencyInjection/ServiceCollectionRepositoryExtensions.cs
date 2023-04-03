﻿using Microsoft.Extensions.DependencyInjection.Extensions;

using Zero.Ddd.Domain.Entities;
using Zero.Ddd.Domain.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionRepositoryExtensions
    {
        public static IServiceCollection AddDefaultRepository(
            this IServiceCollection services,
            Type entityType,
            Type repositoryImplementationType,
            bool replaceExisting = false)
        {
            var repositoryInterface = typeof(IRepository<>).MakeGenericType(entityType);
            if (repositoryInterface.IsAssignableFrom(repositoryImplementationType))
            {
                RegisterService(services, repositoryInterface, repositoryImplementationType, replaceExisting);
            }

            var primaryKeyType = EntityHelper.FindPrimaryKeyType(entityType);
            if (primaryKeyType != null)
            {
                var repositoryInterfaceWithPk = typeof(IRepository<,>).MakeGenericType(entityType, primaryKeyType);
                if (repositoryInterfaceWithPk.IsAssignableFrom(repositoryImplementationType))
                {
                    RegisterService(services, repositoryInterfaceWithPk, repositoryImplementationType, replaceExisting);
                }
            }

            return services;
        }

        private static void RegisterService(
            IServiceCollection services,
            Type serviceType,
            Type implementationType,
            bool replaceExisting)
        {
            if (replaceExisting)
            {
                services.Replace(ServiceDescriptor.Transient(serviceType, implementationType));
            }
            else
            {
                services.TryAddTransient(serviceType, implementationType);
            }
        }
    }
}
