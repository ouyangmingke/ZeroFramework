using DataParser.Domain;

using Microsoft.Extensions.DependencyInjection;

using Zero.Core.Modularity;

namespace Zero.Core.Tests
{
    internal class CoreTestModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddTransient<BaseTestClass, ClassA>();
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(typeof(CoreTestModule), typeof(CoreTestModule))]
    internal class CoreTestModule1 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddTransient<BaseTestClass, ClassB>();
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(typeof(CoreTestModule1))]
    internal class CoreTestModule2 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }

    [DependsOn(
        typeof(CoreTestModule1),
        typeof(CoreTestModule2))]
    internal class CoreTestModule3 : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }
}
