using Microsoft.Extensions.DependencyInjection;

namespace DataParser.Domain
{
    /// <summary>
    /// 基础模块类
    /// </summary>
    public abstract class ZeroModule
    {
        public virtual Task ConfigureServicesAsync(IServiceCollection services)
        {
#if DEBUG
            Console.WriteLine(GetType());
#endif
            return Task.CompletedTask;
        }
    }
}
