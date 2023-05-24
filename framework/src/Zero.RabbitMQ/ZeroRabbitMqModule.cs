using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

using Zero.Core.Modularity;
using Zero.Json;
using Zero.RabbitMQ.Domain;

namespace Zero.RabbitMQ
{
    [DependsOn(
        typeof(ZeroJsonModule)
        )]
    public class ZeroRabbitMqModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            var configuration = services.GetConfiguration();
            services.Configure<ZeroRabbitMqOptions>(configuration.GetSection("RabbitMQ"));
            services.Configure<ZeroRabbitMqOptions>(options =>
           {
               foreach (var connectionFactory in options.Connections.Values)
               {
                   connectionFactory.DispatchConsumersAsync = true;
               }
           });
            AddServices(services);
            return base.ConfigureServicesAsync(services);
        }
        public void AddServices(IServiceCollection services)
        {
            services.AddTransient<IRabbitMqSerializer, Utf8JsonRabbitMqSerializer>();
            services.AddSingleton<IChannelPool, ChannelPool>();
            services.AddSingleton<IConnectionPool, ConnectionPool>();
            services.AddSingleton<IRabbitMqMessageConsumerFactory, RabbitMqMessageConsumerFactory>();

        }
    }
}
