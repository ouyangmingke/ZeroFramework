using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Zero.Ddd.Domain.Repositories;
using Zero.MongoDBTests;
using Zero.TestApp.Domain.Data;
using Zero.TestBase.Testing;

namespace Zero.MongoDB.Domain.Repositories.Tests
{
    [TestClass()]
    public class MongoDbRepositoryTests : ZeroIntegratedTest<MongoDbTestModule>
    {
        public MongoDbRepositoryTests()
        {

        }
        IRepository<Restaurant> Repository => GetRequiredService<IRepository<Restaurant>>();
        [TestMethod()]
        public async Task InsertAsyncTest()
        {
            try
            {
                var a = new Restaurant
                {
                    Name = "陈记烧腊",
                    RestaurantId = "2",
                    Cuisine = "烧鸭",
                    Borough = "广州"
                };
                await Repository.InsertAsync(a);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task GetCountAsyncTest()
        {
            try
            {
                var restaurants = await Repository.GetListAsync(t => t.RestaurantId == "2");
                await Console.Out.WriteLineAsync($"获取到{restaurants.Count}");
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task DeleteAsyncTestAsync()
        {
            try
            {
                var restaurant = await Repository.GetAsync(t => t.RestaurantId == "2");
                await Repository.DeleteAsync(restaurant);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task UpdateAsyncTestAsync()
        {
            try
            {
                var restaurant = await Repository.GetAsync(t => t.RestaurantId == "2");
                restaurant.RestaurantId = "3";
                await Repository.UpdateAsync(restaurant);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                Assert.Fail();
            }
        }
    }
}