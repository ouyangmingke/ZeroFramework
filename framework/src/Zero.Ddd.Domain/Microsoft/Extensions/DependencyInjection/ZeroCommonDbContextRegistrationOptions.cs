using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

using Zero.Ddd.Domain.Entities;
using Zero.Ddd.Domain.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public abstract class ZeroCommonDbContextRegistrationOptions : IZeroCommonDbContextRegistrationOptionsBuilder
    {
        public IServiceCollection Services { get; }
        public Type OriginalDbContextType { get; }
        public Type DefaultRepositoryDbContextType { get; protected set; }
        public Type DefaultRepositoryImplementationType { get; private set; }

        public Type DefaultRepositoryImplementationTypeWithoutKey { get; private set; }
        public bool RegisterDefaultRepositories { get; private set; }

        public bool IncludeAllEntitiesForDefaultRepositories { get; private set; }
        public Dictionary<Type, Type> CustomRepositories { get; }
        /// <summary>
        /// 指定使用 默认储存库 的实体类型
        /// </summary>
        public List<Type> SpecifiedDefaultRepositories { get; }
        public bool SpecifiedDefaultRepositoryTypes => DefaultRepositoryImplementationType != null && DefaultRepositoryImplementationTypeWithoutKey != null;
        protected ZeroCommonDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        {
            OriginalDbContextType = originalDbContextType;
            Services = services;
            DefaultRepositoryDbContextType = originalDbContextType;
            CustomRepositories = new Dictionary<Type, Type>();
            //ReplacedDbContextTypes = new Dictionary<MultiTenantDbContextType, Type>();
            SpecifiedDefaultRepositories = new List<Type>();
        }


        public IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false)
        {
            RegisterDefaultRepositories = true;
            IncludeAllEntitiesForDefaultRepositories = includeAllEntities;
            return this;
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false)
        {
            return AddDefaultRepositories(typeof(TDefaultRepositoryDbContext), includeAllEntities);
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false)
        {
            if (!defaultRepositoryDbContextType.IsAssignableFrom(OriginalDbContextType))
            {
                throw new Exception($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {defaultRepositoryDbContextType.AssemblyQualifiedName}!");
            }

            DefaultRepositoryDbContextType = defaultRepositoryDbContextType;

            return AddDefaultRepositories(includeAllEntities);
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepository<TEntity>()
        {
            return AddDefaultRepository(typeof(TEntity));
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepository(Type entityType)
        {
            if (!SpecifiedDefaultRepositories.Contains(entityType))
            {
                SpecifiedDefaultRepositories.Add(entityType);
            }
            return this;
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>()
        {
            AddCustomRepository(typeof(TEntity), typeof(TRepository));

            return this;
        }
        private void AddCustomRepository(Type entityType, Type repositoryType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity<>).AssemblyQualifiedName}.");
            }

            if (!typeof(IRepository).IsAssignableFrom(repositoryType))
            {
                throw new Exception($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IRepository<>).AssemblyQualifiedName}.");
            }

            CustomRepositories[entityType] = repositoryType;
        }

        public IZeroCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses([NotNull] Type repositoryImplementationType, [NotNull] Type repositoryImplementationTypeWithoutKey)
        {
            DefaultRepositoryImplementationType = repositoryImplementationType;
            DefaultRepositoryImplementationTypeWithoutKey = repositoryImplementationTypeWithoutKey;
            return this;

        }
    }
}
