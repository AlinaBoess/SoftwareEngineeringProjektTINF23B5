using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using System.Net;
using System.Net.Http.Json;


namespace RestaurantReservierung.Tests.Integration
{
    [TestFixture]
    public class RestaurantEndpointTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("RestaurantsIntegrationDb"));

                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        db.Database.EnsureCreated();

                        var _user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 1 };
                        var _rest1 = new Restaurant() { Address = "Am Weg", Name = "Zum Restaurant", OpeningHours = "8-9 Uhr", RestaurantId = 1, Tables = new List<Table>(), User = _user, UserId = 1, Website = "google.com" };
                        var _rest2 = new Restaurant() { Address = "Am Weg2", Name = "Zum Restaurant2", OpeningHours = "8-9 Uhr", RestaurantId = 2, Tables = new List<Table>(), User = _user, UserId = 1, Website = "google2.com" };


                        db.Users.Add(_user);
                        db.Restaurants.Add(_rest1);
                        db.Restaurants.Add(_rest2);
                        db.SaveChanges();
                    });
                });

            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllRestaurants_Endpoint_ReturnsOkAndList()
        {
            var response = await _client.GetAsync("/api/restaurant");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var result = await response.Content.ReadFromJsonAsync<List<Restaurant>>();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(r => r.Name == "Zum Restaurant"));
            Assert.That(result.Any(r => r.Name == "Zum Restaurant2"));
        }
    }
}
