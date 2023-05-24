using Microsoft.Extensions.DependencyInjection;

using Zero.Core.Modularity;
using Zero.EventBus.RabbitMQ;
using Zero.TestApp;

namespace Zero.EventBus.Tests
{
    [DependsOn(
        typeof(TestAppModule),
        typeof(ZeroEventBusRabbitMqModule)
        )]
    public class EventBusTestModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            return base.ConfigureServicesAsync(services);
        }
    }
}
