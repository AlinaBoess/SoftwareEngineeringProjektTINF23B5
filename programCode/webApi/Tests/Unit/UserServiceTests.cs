using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using RestaurantReservierung.Data;
using RestaurantReservierung.Services;
using RestaurantReservierung.Models;
using Microsoft.AspNetCore.Http;

namespace RestaurantReservierung.Tests.Unit
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _service;
        private AppDbContext _dbContext;
        private AuthService _authService;
        private Mock<IConfiguration> _configMock;

        [SetUp]
        public void SetUp()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c["Jwt:Key"]).Returns("ThisShouldBeARelativelyLongAndSecureSecretKeyString");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            _authService = new AuthService(_configMock.Object);

            //get in-memory DB context
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "TestD2B_" + Guid.NewGuid())
               .Options;

            _dbContext = new AppDbContext(options);

            // fresh instances before each test
            _service = new UserService(_dbContext, _authService);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateUserAsync_AddsUser()
        {
            var model = new User { Email = "new@user.com", FirstName = "New", LastName = "User", Password = "passwd", Role = "USER" };
            var added = await _service.RegisterAsync(model);
            Assert.That(added, Is.True);
        }

        [Test]
        public async Task CreateUserAsyncWithInvalidEmail_ThrowsBadHttpRequestException()
        {
            var model = new User { Email = "new@user.c", FirstName = "New", LastName = "User", Password = "passwd", Role = "USER" };
            var ex = Assert.ThrowsAsync<BadHttpRequestException>(() => _service.RegisterAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The given email is not in a valid format"), "Fails if The given email is not in a valid format");
        }

        [Test]
        public async Task CreateUserAsyncWithInsecurePassword_ThrowsBadHttpRequestException()
        {
            var model = new User { Email = "new@user.com", FirstName = "New", LastName = "User", Password = "pass", Role = "USER" };
            var ex = Assert.ThrowsAsync<BadHttpRequestException>(() => _service.RegisterAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The password is shorter than 6 or longer than 64 characters"), "Fails if The password is shorter than 6 or longer than 64 characters");
        }

        [Test]
        public async Task CreateSameUserAsync_ThrowsException()
        {
            var model = new User { Email = "new@user.com", FirstName = "New", LastName = "User", Password = "passwd", Role = "USER" };
            var model2 = new User { Email = "new@user.com", FirstName = "New", LastName = "User", Password = "passwd", Role = "USER" };
            var added = await _service.RegisterAsync(model);
            Assert.That(added, Is.True);
            var ex = Assert.ThrowsAsync<BadHttpRequestException>(() => _service.RegisterAsync(model2));
            Assert.That(ex.Message, Is.EqualTo("There already exists an user with the given email"), "Fails if There already exists an user with the given email");
        }

        [Test]
        public async Task CreateUserAsyncWithTooLongRandomEmail_ThrowsBadHttpRequestException()
        {
            Random r = new Random();
            char[] email = new char[256];
            for (int i = 0; i < 256; ++i)
                email[i] = (char)r.Next((int)'a', ((int)'z') + 1);
            string emailStr = new string(email);
            Assert.That(emailStr.Length, Is.EqualTo(256));

            var model = new User { Email = emailStr + "@test.com", FirstName = "New", LastName = "User", Password = "passwd", Role = "USER" };
            var ex = Assert.ThrowsAsync<BadHttpRequestException>(() => _service.RegisterAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The given email is too long"), "Fails if The given email is too long");
        }

        [Test]
        public async Task GetUserByIdAsync_Existing_ReturnsUser()
        {
            var user = new User() { Email = "a2@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 5 };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var fetched = await _service.GetUserByIdAsync(5);
            Assert.That(fetched, Is.Not.Null);
            Assert.That(fetched.Email, Is.EqualTo("a2@b.com"));
        }
    }

}
