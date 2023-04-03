using Microsoft.VisualStudio.TestTools.UnitTesting;

using Zero.Core.Tests;
using Zero.TestBase.Testing;

namespace Microsoft.Extensions.DependencyInjection.Tests
{
    [TestClass()]
    public class ServiceCollectionApplicationExtensionsTests : ZeroIntegratedTest<CoreTestModule>
    {
        IEnumerable<BaseTestClass> ClassAAA;
        public ServiceCollectionApplicationExtensionsTests()
        {
            try
            {
                ClassAAA = GetRequiredService<IEnumerable<BaseTestClass>>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [TestMethod()]
        public void AddApplicationTest()
        {
          foreach (var service in ClassAAA)
            {
                Console.WriteLine(service.CurrentType()); ;
            }
            //Assert.Fail();
        }

    }
}