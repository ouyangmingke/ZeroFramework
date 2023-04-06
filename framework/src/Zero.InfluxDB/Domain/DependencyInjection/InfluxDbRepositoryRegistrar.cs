using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;

using Zero.InfluxDB.Domain.Repositories;

namespace Zero.InfluxDB.Domain.DependencyInjection
{
    public class InfluxDbRepositoryRegistrar : RepositoryRegistrarBase<ZeroInfluxDbContextRegistrationOptions>
    {
        public InfluxDbRepositoryRegistrar(ZeroInfluxDbContextRegistrationOptions options) : base(options)
        {
        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return InfluxDbContextHelper.GetEntityTypes(dbContextType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(InfluxDbRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override void RegisterCustomRepositories()
        {
            foreach (var customRepository in Options.CustomRepositories)
            {
                RegisterService(Options.Services, customRepository.Key, customRepository.Value, true);
            }
        }

        protected override void RegisterDefaultRepository(Type entityType)
        {
            var implementationType = GetRepositoryType(Options.DefaultRepositoryDbContextType, entityType);
            RegisterService(Options.Services, entityType, implementationType);


        }
        private void RegisterService(
            IServiceCollection services,
            Type entityType,
            Type implementationType,
            bool replaceExisting = false)
        {
            var serviceType = typeof(IInfluxDbRepository<>).MakeGenericType(entityType);
            if (serviceType.IsAssignableFrom(implementationType))
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
}
