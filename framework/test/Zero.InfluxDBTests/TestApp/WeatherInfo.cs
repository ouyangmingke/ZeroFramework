using InfluxDB.Client.Core;

using Zero.Ddd.Domain.Entities;

namespace Zero.InfluxDBTests.TestApp
{
    [Measurement("WeatherInfo")]
    public class WeatherInfo : IEntity
    {
        public WeatherInfo()
        {
            Time = DateTime.UtcNow;
        }
        /// <summary>
        /// 省份
        /// </summary>
        [Column("Province", IsTag = true)] public string? Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Column("City", IsTag = true)] public string? City { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        [Column("Temperature")] public double Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        [Column("Humidity")] public double Humidity { get; set; }

        /// <summary>
        /// 时间戳 需要使用 UTC 时间
        /// </summary>
        [Column(IsTimestamp = true)] public DateTime Time { get; init; }
    }


    [Measurement("Weather")]
    public class Weather : IEntity
    {
        public Weather()
        {
            Time = DateTime.UtcNow;
        }
        /// <summary>
        /// 省份
        /// </summary>
        [Column("Province", IsTag = true)] public string? Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Column("City", IsTag = true)] public string? City { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        [Column("Temperature")] public double Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        [Column("Humidity")] public double Humidity { get; set; }

        /// <summary>
        /// 时间戳 需要使用 UTC 时间
        /// </summary>
        [Column(IsTimestamp = true)] public DateTime Time { get; init; }
    }
}
