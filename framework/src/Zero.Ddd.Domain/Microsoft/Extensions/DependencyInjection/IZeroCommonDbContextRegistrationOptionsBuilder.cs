using JetBrains.Annotations;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IZeroCommonDbContextRegistrationOptionsBuilder {
        IServiceCollection Services { get; }

        //
        // 摘要:
        //     Registers default repositories for all the entities in this DbContext.
        //
        // 参数:
        //   includeAllEntities:
        //     Registers repositories only for aggregate root entities by default. Set includeAllEntities
        //     to true to include all entities.
        IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false);

        //
        // 摘要:
        //     Registers default repositories for all the entities in this DbContext. Default
        //     repositories will use given TDefaultRepositoryDbContext.
        //
        // 参数:
        //   includeAllEntities:
        //     Registers repositories only for aggregate root entities by default. Set includeAllEntities
        //     to true to include all entities.
        //
        // 类型参数:
        //   TDefaultRepositoryDbContext:
        //     DbContext type that will be used by default repositories
        IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false);

        //
        // 摘要:
        //     Registers default repositories for all the entities in this DbContext. Default
        //     repositories will use given defaultRepositoryDbContextType.
        //
        // 参数:
        //   defaultRepositoryDbContextType:
        //     DbContext type that will be used by default repositories
        //
        //   includeAllEntities:
        //     Registers repositories only for aggregate root entities by default. Set includeAllEntities
        //     to true to include all entities.
        IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false);

        //
        // 摘要:
        //     Registers default repository for a specific entity.
        //
        // 类型参数:
        //   TEntity:
        //     Entity type
        IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepository<TEntity>();

        //
        // 摘要:
        //     Registers default repository for a specific entity.
        //
        // 参数:
        //   entityType:
        IZeroCommonDbContextRegistrationOptionsBuilder AddDefaultRepository(Type entityType);

        //
        // 摘要:
        //     Registers custom repository for a specific entity. Custom repositories overrides
        //     default repositories.
        //
        // 类型参数:
        //   TEntity:
        //     Entity type
        //
        //   TRepository:
        //     Repository type
        IZeroCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>();

        /// <summary>
        /// Uses given class(es) for default repositories.
        /// </summary>
        /// <param name="repositoryImplementationType">Repository implementation type</param>
        /// <param name="repositoryImplementationTypeWithoutKey">Repository implementation type (without primary key)</param>
        /// <returns></returns>
        IZeroCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses([NotNull] Type repositoryImplementationType, [NotNull] Type repositoryImplementationTypeWithoutKey);

    }
}
