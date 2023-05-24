using Zero.EventBus.Tests;
using Zero.TestApp.Domain.Data;
using Zero.TestBase.Testing;

namespace Zero.EventBus.Domain.Distributed.Tests
{
    [TestClass()]
    public class DistributedEventBusBaseTests : ZeroIntegratedTest<EventBusTestModule>
    {
        [TestMethod()]
        public async Task PublishAsyncTest()
        {
            var distributedEventBus = GetRequiredService<IDistributedEventBus>();
            await distributedEventBus.PublishAsync(new Restaurant());
        }
    }
}