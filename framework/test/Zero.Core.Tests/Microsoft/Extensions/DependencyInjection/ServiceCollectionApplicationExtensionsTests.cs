using Microsoft.VisualStudio.TestTools.UnitTesting;

using Zero.Core.Tests;
using Zero.TestBase.Testing;

namespace Microsoft.Extensions.DependencyInjection.Tests
{
    [TestClass()]
    public class ServiceCollectionApplicationExtensionsTests : ZeroIntegratedTest<CoreTestModule3>
    {
        IEnumerable<BaseTestClass> BaseTestClasss;
        public ServiceCollectionApplicationExtensionsTests()
        {
            BaseTestClasss = GetRequiredService<IEnumerable<BaseTestClass>>();
        }
        [TestMethod()]
        public void AddApplicationTest()
        {
            foreach (var service in BaseTestClasss)
            {
                Console.WriteLine(service.CurrentType()); ;
            }
        }

    }
}