using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.IO;
using RestaurantReservierung.Data;

namespace RestaurantReservierung.Tests.Integration
{
    [TestFixture]
    public class DatabaseTest
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            string webApiDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

            var config = new ConfigurationBuilder()
                .SetBasePath(webApiDirectory)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 11, 11)))
                          .UseLazyLoadingProxies();

            _context = new AppDbContext(optionsBuilder.Options);
        }

        [Test]
        public void GetAllRestaurants_ShouldReturnResults()
        {
            var restaurants = _context.Restaurants.ToList();

            Assert.That(restaurants, Is.Not.Null);
            Assert.That(restaurants.Count, Is.GreaterThan(0));

            TestContext.WriteLine($"Found {restaurants.Count} restaurants");
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
