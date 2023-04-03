using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;
using Zero.Core.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApplicationExtensions
    {
        static readonly List<Type> moduleTypes = new() { };
        public static IServiceCollection AddApplication<TStartupModule>(
            [NotNull] this IServiceCollection services)
            where TStartupModule : ZeroModule
        {

            FindBundleContributorsRecursively(typeof(TStartupModule), moduleTypes);

            foreach (var item in moduleTypes)
                services.AddSingleton(typeof(ZeroModule), item);
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
        /// <param name="type"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public static void FindBundleContributorsRecursively(Type type, List<Type> models)
        {
            if (models.Any(t => t.FullName == type.FullName))
                return;
            models.Add(type);
            // 取出关联模块属性
            var dependedTypes = type.GetCustomAttributes().OfType<IDependedTypesProvider>().ToList();
            // 取出属性中的模块 Type
            foreach (var item in dependedTypes.SelectMany(t => t.GetDependedTypes()).ToList())
                FindBundleContributorsRecursively(item, models);
        }
    }
}
