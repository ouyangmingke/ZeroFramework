using Microsoft.Extensions.DependencyInjection;

using System;

namespace Zero.InfluxDB.Domain.DependencyInjection
{
    public class ZeroInfluxDbContextRegistrationOptions : ZeroCommonDbContextRegistrationOptions, IZeroInfluxDbContextRegistrationOptionsBuilder
    {
        public ZeroInfluxDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services) : base(originalDbContextType, services)
        {
        }
    }
}
