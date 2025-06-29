using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Tests.Unit
{
    [TestFixture]
    public class RestaurantServiceTests
    {
        private RestaurantService _service;
        private AppDbContext inMemoryDBContext;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            //get in-memory DB context
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDB3_" + Guid.NewGuid())
               .Options;

            inMemoryDBContext = new AppDbContext(options);

            // fresh instances before each test
            _service = new RestaurantService(inMemoryDBContext);

            _user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };

        }

        [TearDown]
        public void TearDown()
        {
            inMemoryDBContext.Dispose();
        }

        [Test]
        public async Task AddRestaurantAsync_CreatesRestaurant()
        {
            var model = new RestaurantFormModel
            {
                Name = "NewPlace",
                Adress = "Street 1",
                OpeningHours = "9-17",
                Website = "http://example.com"
            };

            var added = await _service.AddRestaurantAsync(model, _user);
            Assert.That(added, Is.Not.Null);
            Assert.That(added.Name, Is.EqualTo("NewPlace"));
            Assert.That(added.UserId, Is.EqualTo(_user.UserId));
        }

        [Test]
        public async Task GetManyRestaurantsAsync_ReturnsAll()
        {
            var _user = new User() {  Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            inMemoryDBContext.Users.Add(_user);
            inMemoryDBContext.Restaurants.AddRange(
                new Restaurant { RestaurantId = 1, Name = "A", UserId = _user.UserId },
                new Restaurant { RestaurantId = 2, Name = "B", UserId = _user.UserId }
            );
            await inMemoryDBContext.SaveChangesAsync();

            var list = await _service.GetManyRestaurantsAsync(new GetManyRestaurantFormModel { });
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetRestaurantByIdAsync_Existing_ReturnsRestaurant()
        {
            var rest = new Restaurant { RestaurantId = 5, Name = "C", UserId = _user.UserId };
            inMemoryDBContext.Restaurants.Add(rest);
            await inMemoryDBContext.SaveChangesAsync();

            var fetched = await _service.GetRestaurantByIdAsync(5);
            Assert.That(fetched, Is.Not.Null);
            Assert.That(fetched.Name, Is.EqualTo("C"));
        }

        [Test]
        public async Task DeleteRestaurantAsync_RemovesRestaurant()
        {
            var rest = new Restaurant { RestaurantId = 10, Name = "ToRemove", UserId = _user.UserId };
            inMemoryDBContext.Restaurants.Add(rest);
            await inMemoryDBContext.SaveChangesAsync();

            await _service.DeleteRestaurantAsync(rest);
            var found = await inMemoryDBContext.Restaurants.FindAsync(10);
            Assert.That(found, Is.Null);
        }

        [Test]
        public async Task GetUserRestaurantsAsync_ReturnsCorrectOwnedRestaurants()
        {
            var _user2 = new User() { Email = "a2@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            
            inMemoryDBContext.Restaurants.AddRange(
                new Restaurant { RestaurantId = 20, Name = "R1", User = _user },
                new Restaurant { RestaurantId = 21, Name = "R2", User = _user2 }
            );
            await inMemoryDBContext.SaveChangesAsync();

            var result = await _service.GetUserRestaurantsAsync(_user);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("R1"));
        }

        [Test]
        public async Task UpdateRestaurantAsync_UpdatesRestaurantFields()
        {
            var rest = new Restaurant { RestaurantId = 50, Name = "Old", Address = "OldAddr", OpeningHours = "9-12", Website = "url", UserId = _user.UserId };
            inMemoryDBContext.Restaurants.Add(rest);
            await inMemoryDBContext.SaveChangesAsync();

            var updateModel = new RestaurantFormModel
            {
                Name = "New",
                Adress = "NewAddr",
                OpeningHours = "10-18",
                Website = "newurl"
            };

            await _service.UpdateRestaurantAsync(rest, updateModel);
            var updated = await inMemoryDBContext.Restaurants.FindAsync(50);

            Assert.That(updated.Name, Is.EqualTo("New"));
            Assert.That(updated.Address, Is.EqualTo("NewAddr"));
            Assert.That(updated.OpeningHours, Is.EqualTo("10-18"));
            Assert.That(updated.Website, Is.EqualTo("newurl"));
        }
    }
}