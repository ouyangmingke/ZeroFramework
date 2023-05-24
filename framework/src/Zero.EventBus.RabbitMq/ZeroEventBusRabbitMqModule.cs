using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

using Zero.Core.Modularity;
using Zero.EventBus.Domain.Distributed;
using Zero.EventBus.RabbitMQ.Domain;
using Zero.RabbitMQ;

namespace Zero.EventBus.RabbitMQ;

[DependsOn(
    typeof(ZeroEventBusModule),
    typeof(ZeroRabbitMqModule))]
public class ZeroEventBusRabbitMqModule : ZeroModule
{
    public override Task ConfigureServicesAsync(IServiceCollection services)
    {
        services.AddSingleton<IDistributedEventBus, RabbitMqDistributedEventBus>();
        var configuration = services.GetConfiguration();
        services.Configure<ZeroRabbitMqEventBusOptions>(configuration.GetSection("RabbitMQ:EventBus"));
        return base.ConfigureServicesAsync(services);
    }
}
