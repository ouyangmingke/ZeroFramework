using InfluxDB.Client;

using System;

namespace Zero.InfluxDB
{
    public class ZeroInfluxDbContextOptions
    {
        public Action<InfluxDBClientOptions> InfluxDBClientOptionsConfigurer { get; set; }
    }
}
