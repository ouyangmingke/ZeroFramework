using System.Threading.Tasks;

namespace Zero.Data.Domain
{
    public interface IConnectionStringResolver
    {
        Task<string> ResolveAsync(string connectionStringName = null);
    }
}