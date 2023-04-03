using Microsoft.Extensions.DependencyInjection;

namespace Zero.MongoDB.Domain.DependencyInjection
{
    public class ZeroMongoDbContextRegistrationOptions : ZeroCommonDbContextRegistrationOptions, IZeroMongoDbContextRegistrationOptionsBuilder
    {
        public ZeroMongoDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services) : base(originalDbContextType, services)
        {
        }
    }
}
