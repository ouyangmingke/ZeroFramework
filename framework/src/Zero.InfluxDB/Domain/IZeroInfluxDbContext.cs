using InfluxDB.Client;

namespace Zero.InfluxDB.Domain
{
    public interface IZeroInfluxDbContext
    {
        InfluxDBClient Client { get; }
        public string Bucket { get; }
        public string Org { get; }
    }
}
