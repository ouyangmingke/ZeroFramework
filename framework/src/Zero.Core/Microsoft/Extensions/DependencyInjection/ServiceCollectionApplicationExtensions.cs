using JetBrains.Annotations;

using System.Reflection;

using Zero.Bundling;
using Zero.Core.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApplicationExtensions
    {
        static readonly List<BundleTypeDefinition> moduleTypes = new() { };
        public static IServiceCollection AddApplication<TStartupModule>(
            [NotNull] this IServiceCollection services)
            where TStartupModule : ZeroModule
        {

            FindBundleContributorsRecursively(typeof(TStartupModule), 0, moduleTypes);

            foreach (var module in moduleTypes.OrderByDescending(t => t.Level))
            {
                services.AddSingleton(typeof(ZeroModule), module.BundleContributorType);
            }
            var serviceProvider = services.BuildServiceProvider();

            var modules = serviceProvider.GetRequiredService<IEnumerable<ZeroModule>>();
            foreach (var module in modules)
            {
                module.ConfigureServicesAsync(services);
            }
            return services;
        }

        /// <summary>
        /// 递归获取全部模块
        /// </summary>
        /// <param name="module"></param>
        /// <param name="moduls"></param>
        /// <returns></returns>
        private static void FindBundleContributorsRecursively(Type module, int level, List<BundleTypeDefinition> moduls)
        {
            var definition = moduls.FirstOrDefault(t => t.BundleContributorType.FullName == module.FullName);
            if (definition != null)
            {
                definition.Level = level;
            }
            else
            {
                moduls.Add(new BundleTypeDefinition { Level = level, BundleContributorType = module });
            }
            // 取出关联模块属性
            var dependedTypes = module.GetCustomAttributes().OfType<IDependedTypesProvider>();

            // 取出属性中的模块 Type
            foreach (var item in dependedTypes.SelectMany(t => t.GetDependedTypes()))
                FindBundleContributorsRecursively(item, ++level, moduls);
        }
    }
}
