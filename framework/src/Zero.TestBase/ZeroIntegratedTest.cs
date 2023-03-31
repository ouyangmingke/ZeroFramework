using Microsoft.Extensions.DependencyInjection;

namespace Zero.TestBase
{
    /// <summary>
    /// Zero 单元测试基类
    /// </summary>
    public abstract class ZeroIntegratedTest : ZeroTestBaseWithServiceProvider
    {
        public ZeroIntegratedTest()
        {
            var services = CreateServiceCollection();
            AddServer(services);
            ServiceProvider = CreateServiceProvider(services);
        }

        protected virtual IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }
        protected abstract void AddServer(IServiceCollection services);
        protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
