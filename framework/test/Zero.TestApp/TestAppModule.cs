using Zero.Core.Modularity;
using Zero.Ddd.Domain;
using Zero.TestBase;

namespace Zero.TestApp
{
    [DependsOn(
        typeof(ZeroDddDomainModule),
        typeof(ZeroTestBaseModule)
        )]
    public class TestAppModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }
}
