using DataParser.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Threading.Tasks;

using Zero.Data.Domain;

namespace Zero.Data
{
    public class ZeroDataModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.TryAddTransient(
                typeof(IConnectionStringResolver),
                typeof(DefaultConnectionStringResolver)
                );

            return base.ConfigureServicesAsync(services);
        }
    }
}
