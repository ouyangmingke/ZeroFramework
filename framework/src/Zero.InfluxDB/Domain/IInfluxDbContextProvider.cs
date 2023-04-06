using System.Threading;
using System.Threading.Tasks;

namespace Zero.InfluxDB.Domain
{
    public interface IInfluxDbContextProvider<TInfluxDbContext>
        where TInfluxDbContext : IZeroInfluxDbContext
    {
        Task<TInfluxDbContext> GetDbContextAsync(CancellationToken cancellationToken = default);
    }
}
