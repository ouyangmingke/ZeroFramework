using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Threading.Tasks;

using Zero.Core.Modularity;
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
            services.TryAddTransient(
                typeof(IDataFilter),
                typeof(DataFilter)
                );

            return base.ConfigureServicesAsync(services);
        }
    }
}
