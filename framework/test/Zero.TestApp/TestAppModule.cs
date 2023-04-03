using Zero.Core.Modularity;
using Zero.Ddd.Domain;
using Zero.TestBase;

namespace Zero.TestApp
{
    [DependsOn(
        typeof(ZeroDddDomainModule),
        typeof(ZeroTestBaseModule)
        )]
    public class TestAppModule : ZeroModule
    {
    }
}
