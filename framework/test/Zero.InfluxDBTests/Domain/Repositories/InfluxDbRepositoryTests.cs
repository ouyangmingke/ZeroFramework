using InfluxDB.Client.Api.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Zero.InfluxDBTests;
using Zero.InfluxDBTests.TestApp;
using Zero.TestBase.Testing;

namespace Zero.InfluxDB.Domain.Repositories.Tests
{
    [TestClass()]
    public class InfluxDbRepositoryTests : ZeroIntegratedTest<InfluxDbTestModule>
    {
        IInfluxDbRepository<WeatherInfo> WeatherInfoRepository =>
            GetRequiredService<IInfluxDbRepository<WeatherInfo>>();
        IInfluxDbRepository<Weather> WeatherRepository =>
          GetRequiredService<IInfluxDbRepository<Weather>>();

        [TestMethod()]
        public async Task InsertAsyncTest()
        {
            try
            {
                await WeatherRepository.InsertAsync(new Weather { Province = "湖北", City = "武汉", Temperature = 24, Humidity = 61 });
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task InsertManyAsyncTest()
        {
            try
            {
                var weatherInfos = new List<WeatherInfo> {
                new WeatherInfo{ Province = "湖北", City = "武汉", Temperature = 24, Humidity = 61 },
                new WeatherInfo{ Province = "湖北", City = "武汉", Temperature = 26, Humidity = 55 },
                new WeatherInfo{ Province = "广东", City = "广州", Temperature = 20, Humidity = 69 },
                new WeatherInfo{ Province = "广东", City = "广州", Temperature = 28, Humidity = 60 }
                };
                await WeatherInfoRepository.InsertManyAsync(weatherInfos);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task GetListAsyncTestAsync()
        {
            try
            {
                var flux = "from(bucket: \"DotNetBucket\") |> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"Weather\")";
                var weathers = await WeatherRepository.GetListAsync(flux);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task DeleteAsyncTestAsync()
        {
            try
            {
                var request = new DeletePredicateRequest(DateTime.UtcNow.AddMinutes(-1), DateTime.Now);
                await WeatherInfoRepository.DeleteAsync(request);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }
    }
}