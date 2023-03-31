using Microsoft.Extensions.Options;

using System.Threading.Tasks;

using Zero.Core.DependencyInjection;

namespace Zero.Data.Domain
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        protected ZeroDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(
            IOptionsMonitor<ZeroDbConnectionOptions> options)
        {
            Options = options.CurrentValue;
        }

        public virtual Task<string> ResolveAsync(string connectionStringName = null)
        {
            return Task.FromResult(ResolveInternal(connectionStringName));
        }

        private string ResolveInternal(string connectionStringName)
        {
            if (connectionStringName == null)
            {
                return Options.ConnectionStrings.Default;
            }

            var connectionString = Options.GetConnectionStringOrNull(connectionStringName);

            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            return null;
        }
    }
}