using Microsoft.Extensions.DependencyInjection;

using Zero.Core.Modularity;
using Zero.Data.Domain;
using Zero.InfluxDB;
using Zero.InfluxDB.Domain.Repositories;
using Zero.InfluxDBTests.TestApp;

namespace Zero.InfluxDBTests
{
    [DependsOn(typeof(ZeroInfluxDbModule))]
    public class InfluxDbTestModule : ZeroModule
    {
        public override Task ConfigureServicesAsync(IServiceCollection services)
        {

            services.AddInfluxDbContext<TestAppInfluxDBDbContext>(option =>
            {
                //option.AddDefaultRepositories();
                option.AddRepository<WeatherInfo, WeatherRepository>();
                option.AddDefaultRepository<Weather>();
            });

            services.Configure<ZeroDbConnectionOptions>(t =>
                t.ConnectionStrings.Default = "http://localhost:8086"
                );
            services.Configure<ZeroInfluxDbContextOptions>(t =>
            {
                t.InfluxDBClientOptionsConfigurer = (options) =>
                {
                    options.Bucket = "DotNetBucket";
                    options.Org = "DotNet";
                    options.Username = "sa";
                    options.Password = "public123";
                    //options.Token = "";
                };
            });

            return base.ConfigureServicesAsync(services);
        }
    }
}
