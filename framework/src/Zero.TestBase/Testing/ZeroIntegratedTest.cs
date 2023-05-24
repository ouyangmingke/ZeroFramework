using Microsoft.Extensions.DependencyInjection;
using Zero.Core.Modularity;

namespace Zero.TestBase.Testing
{
    /// <summary>
    /// Zero 单元测试基类
    /// </summary>
    public abstract class ZeroIntegratedTest<TStartupModule> : ZeroTestBaseWithServiceProvider
          where TStartupModule : ZeroModule
    {
        public ZeroIntegratedTest()
        {
            var services = CreateServiceCollection();
            services.AddApplication<TStartupModule>(SetZeroApplicationCreationOptions);
            ServiceProvider = CreateServiceProvider(services);
        }

        protected virtual IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }
        protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
        protected virtual void SetZeroApplicationCreationOptions(ZeroApplicationCreationOptions options)
        {

        }
    }
}
