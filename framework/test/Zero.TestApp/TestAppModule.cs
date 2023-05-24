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
            var path = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(Path.Combine("Config", "appsettings.json"))
                .Build();
            services.ReplaceConfiguration(configuration);
            return base.ConfigureServicesAsync(services);
        }
    }
}
