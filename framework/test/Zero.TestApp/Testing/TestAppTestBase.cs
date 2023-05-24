using Zero.Core.Modularity;
using Zero.TestBase.Testing;

namespace Zero.TestApp.Testing
{
    public abstract class TestAppTestBase<TStartupModule> : ZeroIntegratedTest<TStartupModule>
        where TStartupModule : ZeroModule
    {
        protected override void SetZeroApplicationCreationOptions(ZeroApplicationCreationOptions options)
        {
            options.Configuration.BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
        }
    }
}
