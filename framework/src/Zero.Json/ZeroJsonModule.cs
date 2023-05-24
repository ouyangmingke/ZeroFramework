using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

using Zero.Core.Modularity;
using Zero.Json.Domain;
using Zero.Json.SystemTextJson;

namespace Zero.Json
{
    public class ZeroJsonModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddTransient<IJsonSerializer, ZeroSystemTextJsonSerializer>();
            return base.ConfigureServicesAsync(services);
        }
    }
}
