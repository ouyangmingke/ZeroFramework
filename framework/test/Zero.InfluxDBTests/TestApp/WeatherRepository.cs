using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Zero.InfluxDB.Domain;
using Zero.InfluxDB.Domain.Repositories;

namespace Zero.InfluxDBTests.TestApp
{
    public class WeatherRepository : InfluxDbRepository<TestAppInfluxDBDbContext, WeatherInfo>
    {
        public WeatherRepository(IInfluxDbContextProvider<TestAppInfluxDBDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<WeatherInfo> InsertAsync(WeatherInfo entity, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync("WeatherRepository");
            return await base.InsertAsync(entity, cancellationToken);
        }
    }
}
