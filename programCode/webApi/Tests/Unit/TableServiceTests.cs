using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;


namespace RestaurantReservierung.Tests.Unit
{
    [TestFixture]
    public class TableServiceTests
    {
        private TableService _service;
        private AppDbContext inMemoryDBContext;

        [SetUp]
        public void SetUp()
        {
            //get in-memory DB context
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "TestD2B_" + Guid.NewGuid())
               .Options;

            inMemoryDBContext = new AppDbContext(options);

            // fresh instances before each test
            _service = new TableService(inMemoryDBContext);
        }

        [TearDown]
        public void TearDown()
        {
            inMemoryDBContext.Dispose();
        }

        [Test]
        public async Task AddTableAsync_CreatesTable()
        {
            var _user = new User() { Email = "a2@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            var rest = new Restaurant { RestaurantId = 50, Name = "Old", Address = "OldAddr", OpeningHours = "9-12", Website = "url", UserId = _user.UserId };

            var model = new TableFormModel { Area = "Main", Capacity = 4, TableNr = 1 };
            var added = await _service.AddTableAsync(model, rest.RestaurantId);
            Assert.That(added, Is.True);
        }

        [Test]
        public async Task GetTablesForRestaurantAsync_ReturnsTables()
        {
            inMemoryDBContext.Tables.AddRange(
                new Table { TableId = 1, Area = "A", Capacity = 2, RestaurantId = 100 },
                new Table { TableId = 2, Area = "B", Capacity = 4, RestaurantId = 100 }
            );
            await inMemoryDBContext.SaveChangesAsync();

            var list = await _service.GetAllTablesAsync(100);
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteTableAsync_RemovesTable()
        {
            var table = new Table { TableId = 10, Area = "X", Capacity = 6, RestaurantId = 100 };
            inMemoryDBContext.Tables.Add(table);
            await inMemoryDBContext.SaveChangesAsync();

            await _service.RemoveTableAsync(table);
            var found = await inMemoryDBContext.Tables.FindAsync(10);
            Assert.That(found, Is.Null);
        }

    }
}
