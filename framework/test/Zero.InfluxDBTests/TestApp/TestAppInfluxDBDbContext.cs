using Zero.Data.Domain;
using Zero.InfluxDB.Domain;

namespace Zero.InfluxDBTests.TestApp
{
    [ConnectionStringName("Zero"),
        InfluxOrganization("DotNet")]
    public class TestAppInfluxDBDbContext : ZeroInfluxDbContext
    {
        public IInfluxBucket<WeatherInfo> WeatherInfos { get; set; }
        public IInfluxBucket<Weather> Weathers { get; set; }
    
    }
}