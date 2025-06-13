using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;


namespace RestaurantReservierung.Tests.Unit
{
    [TestFixture]
    public class FeedbackServiceTests
    {
        private FeedbackService _service;
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
            _service = new FeedbackService(inMemoryDBContext);
        }

        [TearDown]
        public void TearDown()
        {
            inMemoryDBContext.Dispose();
        }

        [Test]
        public void OwnsFeedback_WhenUserMatches_ReturnsTrue()
        {
            var user = new User { };
            var feedback = new Feedback { User = user };
            var owns = _service.OwnsFeedback(user, feedback);
            Assert.That(owns, Is.True, "User should own their own feedback");
        }

        [Test]
        public void OwnsFeedback_WhenUserDoesNotMatch_ReturnsFalse()
        {
            var user = new User { FirstName = "User1" };
            var otherUser = new User { FirstName = "User2" };
            var feedback = new Feedback { User = otherUser };
            var owns = _service.OwnsFeedback(user, feedback);
            Assert.That(owns, Is.False, "User should not own feedback belonging to another user");
        }

        [Test]
        public async Task CreateFeedbackAsync_AddsEntry()
        {
            var user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            var fb = new Feedback { FeedbackId = 0, UserId = user.UserId, RestaurantId = 1, Rating = 5, Comment = "Great" };
            var restaurant = new Restaurant() { Address = "Am Weg", Name = "Zum Restaurant", OpeningHours = "8-9 Uhr", RestaurantId = 1, Tables = new List<Table>(), User = user, UserId = 0, Website = "google.com" };

            var fbModel = new FeedbackFormModel
            {
                Comment = "Great food",
                Rating = 5
            };

            var _table = new Table()
            {
                Area = "in a room",
                Capacity = 27,
                Reservations = new List<Reservation>(),
                Restaurant = restaurant
            };

            var reservation = new Reservation
            {
                CreatedAt = DateTime.Now,
                EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                UpdatedAt = DateTime.Now,
                Feedbacks = new List<Feedback>(),
                ReservationId = 1,
                StartTime = DateTime.Now,
                Table = _table,
                User = user,
                UserId = user.UserId,
                TableId = _table.TableId
            };

            await _service.CreateFeedbackAsync(user, fbModel, restaurant, reservation);
            
            var saved = await inMemoryDBContext.Feedbacks.FirstOrDefaultAsync(f => f.RestaurantId == 1);
            Assert.That(saved, Is.Not.Null);
            Assert.That(saved.Comment, Is.EqualTo("Great food"));
            Assert.That(saved.Rating, Is.EqualTo(5));
        }

        [Test]
        public async Task CalcRestaurantRatingAsync_WithFeedback_ReturnsAverage()
        {
            var user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            var fb = new Feedback { FeedbackId = 0, UserId = user.UserId, RestaurantId = 1, Rating = 5, Comment = "Great" };
            var restaurant = new Restaurant() { Address = "Am Weg", Name = "Zum Restaurant", OpeningHours = "8-9 Uhr", RestaurantId = 1, Tables = new List<Table>(), User = user, UserId = 0, Website = "google.com" };

            var fbModel1 = new FeedbackFormModel
            {
                Comment = "Great food",
                Rating = 5
            };

            var fbModel2 = new FeedbackFormModel
            {
                Comment = "Good",
                Rating = 3
            };

            var fbModel3 = new FeedbackFormModel
            {
                Comment = "Bad",
                Rating = 2
            };

            var _table = new Table()
            {
                Area = "in a room",
                Capacity = 27,
                Reservations = new List<Reservation>(),
                Restaurant = restaurant
            };

            var reservation = new Reservation
            {
                CreatedAt = DateTime.Now,
                EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                UpdatedAt = DateTime.Now,
                Feedbacks = new List<Feedback>(),
                ReservationId = 1,
                StartTime = DateTime.Now,
                Table = _table,
                User = user,
                UserId = user.UserId,
                TableId = _table.TableId
            };

            await _service.CreateFeedbackAsync(user, fbModel1, restaurant, reservation);
            await _service.CreateFeedbackAsync(user, fbModel2, restaurant, reservation);
            await _service.CreateFeedbackAsync(user, fbModel3, restaurant, reservation);


            var avg = await _service.CalcRestaurantRatingAsync(restaurant);
            Assert.That(avg, Is.EqualTo((5+3+2)/3d));
        }

        [Test]
        public async Task CalcRestaurantRatingAsync_NoFeedback_ReturnsZero()
        {
            var user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
            var restaurant = new Restaurant() { Address = "Am Weg", Name = "Zum Restaurant", OpeningHours = "8-9 Uhr", RestaurantId = 1, Tables = new List<Table>(), User = user, UserId = 0, Website = "google.com" };
            var avg = await _service.CalcRestaurantRatingAsync(restaurant);
            Assert.That(avg, Is.EqualTo(0));
        }

    }
}
