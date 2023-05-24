using Microsoft.Extensions.DependencyInjection;

using Zero.Core.Modularity;

namespace Zero.Core.Tests
{
    public class CoreTestModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddTransient<BaseTestClass, ClassA>();
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(typeof(CoreTestModule))]
    public class CoreTestModule1 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddTransient<BaseTestClass, ClassB>();
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(typeof(CoreTestModule1))]
    public class CoreTestModule2 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(
        typeof(CoreTestModule2))]
    public class CoreTestModule3 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }
}
