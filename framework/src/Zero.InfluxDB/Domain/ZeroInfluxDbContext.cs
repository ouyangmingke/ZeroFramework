using InfluxDB.Client;

namespace Zero.InfluxDB.Domain
{
    public class ZeroInfluxDbContext : IZeroInfluxDbContext
    {
        public InfluxDBClient Client { get; private set; }

        public string Bucket { get; private set; }

        public string Org { get; private set; }

        public virtual void InitializeDatabase(InfluxDBClient client, InfluxDBClientOptions options)
        {
            Bucket = options.Bucket;
            Org = options.Org;
            Client = client;
        }

    }
}
