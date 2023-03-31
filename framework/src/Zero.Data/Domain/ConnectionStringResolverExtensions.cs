using System;
using System.Threading.Tasks;

using Zero.Data.Domain;

namespace Zero.Data.Domain
{
    public static class ConnectionStringResolverExtensions
    {
        public static Task<string> ResolveAsync<T>(this IConnectionStringResolver resolver)
        {
            return resolver.ResolveAsync(typeof(T));
        }

        public static Task<string> ResolveAsync(this IConnectionStringResolver resolver, Type type)
        {
            return resolver.ResolveAsync(ConnectionStringNameAttribute.GetConnStringName(type));
        }
    }
}