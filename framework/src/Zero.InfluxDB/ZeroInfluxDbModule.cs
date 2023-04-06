using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Threading.Tasks;

using Zero.Core.Modularity;
using Zero.Data;
using Zero.Ddd.Domain;
using Zero.InfluxDB.Domain;

namespace Zero.InfluxDB
{
    [DependsOn(
        typeof(ZeroDataModule),
        typeof(ZeroDddDomainModule))]
    public class ZeroInfluxDbModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.TryAddTransient(
    typeof(IInfluxDbContextProvider<>),
    typeof(InfluxDbContextProvider<>)
    );
            services.AddTransient<IZeroInfluxDbContext, ZeroInfluxDbContext>();
            //services.AddTransient<IInfluxModelSource, InfluxModelSource>();
            return base.ConfigureServicesAsync(services);
        }
    }
}
