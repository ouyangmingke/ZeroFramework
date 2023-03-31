using Microsoft.Extensions.DependencyInjection;

namespace Zero.TestBase
{
    /// <summary>
    /// Zero 单元测试服务提供器
    /// </summary>
    public abstract class ZeroTestBaseWithServiceProvider
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected virtual T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected virtual T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}