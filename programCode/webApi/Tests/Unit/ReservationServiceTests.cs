using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Tests.Unit;

[TestFixture]
public class ReservationServiceTests
{
    private ReservationService _service;
    private AppDbContext inMemoryDBContext;

    private Restaurant _restaurant;
    private User _user;
    private Reservation _reservation, _reservation2, _reservation3;
    private Table _table, _table2;


    [SetUp]
    public void SetUp()
    {
        //get in-memory DB context
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDB22_" + Guid.NewGuid())
           .Options;

        inMemoryDBContext = new AppDbContext(options);

        // fresh instances before each test
        _service = new ReservationService(inMemoryDBContext);



        _user = new User() {  Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 0 };
        _restaurant = new Restaurant() { Address = "Am Weg", Name = "Zum Restaurant", OpeningHours = "8-9 Uhr", RestaurantId = 1, Tables = new List<Table>(), User = _user, UserId = 0, Website = "google.com" };
        _table = new Table()
        {
            Area = "in a room",
            Capacity = 27,
            Reservations = new List<Reservation>(),
            Restaurant = _restaurant
        };

        _table2 = new Table()
        {
            Area = "in another room",
            Capacity = 69,
            Reservations = new List<Reservation>(),
            Restaurant = _restaurant
        };

        _reservation = new Reservation
        {
            CreatedAt = DateTime.Now,
            EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
            UpdatedAt = DateTime.Now,
            Feedbacks = new List<Feedback>(),
            ReservationId = 1,
            StartTime = DateTime.Now,
            Table = _table,
            User = _user,
            UserId = _user.UserId,
            TableId = _table.TableId
        };

        _reservation2 = new Reservation
        {
            CreatedAt = DateTime.Now,
            EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
            UpdatedAt = DateTime.Now,
            Feedbacks = new List<Feedback>(),
            ReservationId = 2,
            StartTime = DateTime.Now,
            Table = _table,
            User = _user,
            UserId = _user.UserId,
            TableId = _table.TableId
        };

        _reservation3 = new Reservation
        {
            CreatedAt = DateTime.Now,
            EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
            UpdatedAt = DateTime.Now,
            Feedbacks = new List<Feedback>(),
            ReservationId = 3,
            StartTime = DateTime.Now,
            Table = _table2,
            User = _user,
            UserId = _user.UserId,
            TableId = _table2.TableId
        };

        inMemoryDBContext.Reservations.Add(_reservation);
        inMemoryDBContext.Reservations.Add(_reservation2);
        inMemoryDBContext.Reservations.Add(_reservation3);


        inMemoryDBContext.SaveChanges();

    }

    [TearDown]
    public void TearDown()
    {
        inMemoryDBContext.Dispose();
    }

    [Test]
    public void AddSameReservationTwice_ShouldThrowError()
    {
        try
        {
            inMemoryDBContext.Reservations.Add(_reservation);
            inMemoryDBContext.SaveChanges();

            //fail test if no error was thrown
            Assert.That(false);
        }
        catch (Exception e)
        {
            Assert.That(e.Message.Equals("An item with the same key has already been added. Key: 1"));
        }

    }

    [Test]
    public void AddReservation_ShouldStoreReservation()
    {
        var reservation4 = new Reservation
        {
            CreatedAt = DateTime.Now,
            EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
            UpdatedAt = DateTime.Now,
            Feedbacks = new List<Feedback>(),
            ReservationId = 4,
            StartTime = DateTime.Now,
            Table = _table2,
            User = _user,
            UserId = _user.UserId,
            TableId = _table2.TableId
        };

        inMemoryDBContext.Reservations.Add(reservation4);
        inMemoryDBContext.SaveChanges();
 
        Assert.That(reservation4, Is.Not.Null, "Reservation should not be null");
        Assert.That(inMemoryDBContext.Reservations.Count(), Is.EqualTo(4));
        Assert.That(reservation4.User.FirstName, Is.EqualTo("a"));
    }

    [Test]
    public void GetReservationById_ShouldReturnExisting()
    {
        var reservation = _service.GetReservationByIdAsync(1);

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.ReservationId, Is.EqualTo(1));

    }

    [Test]
    public void GetReservationForRestaurants_ShouldReturnExisting()
    {
        var reservation = _service.GetReservationsForRestaurantsAsync(new List<Restaurant>() { _restaurant }, new Controllers.ReservationFilterModel() { RestaurantId = _restaurant.RestaurantId });

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.Count, Is.EqualTo(3));
    }

    [Test]
    public void GetReservationsForTimeInterval_ShouldReturnExisting()
    {
        var reservation = _service.GetReservationsForTimeIntervalAsync(new Controllers.ReservationFormModel() { StartTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)), EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(5)) }, _table);

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.Count, Is.EqualTo(2));

        for (int i = 0; i < 2; ++i)
            Assert.That(reservation.Result[i].Table, Is.EqualTo(_table));
    }

    [Test]
    public void MinReservationTime_DefaultValue_Is30Minutes()
    {
        // Assuming min time is defined as a static value, e.g., TimeSpan.FromMinutes(30)
        var min = _service.MinReservationTime;
        Assert.That(min.TotalMinutes, Is.GreaterThanOrEqualTo(30), "MinReservationTime should be at least 30 minutes");
    }

    [Test]
    public void IsGreaterThenMinTimeInterval_WithTooShortInterval_ReturnsFalse()
    {
        var form = new ReservationFormModel
        { 
            StartTime = DateTime.Now.AddMinutes(10),
        };
        var result = _service.IsGreaterThenMinTimeInterval(form);
        Assert.That(result, Is.False, "Should be false when reservation interval is below minimum");
    }

    [Test]
    public void IsInPast_WithPastTime_ReturnsTrue()
    {
        var form = new ReservationFormModel { StartTime = DateTime.Now.AddHours(-1) };
        var result = _service.IsInPast(form);
        Assert.That(result, Is.True, "Should detect past reservation times");
    }

    [Test]
    public async Task ReserveAsync()
    {
        var _user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 1 };
        var table = new Table { TableId = 10, Area = "X", Capacity = 6, RestaurantId = 100 };

        var form = new ReservationFormModel { StartTime = DateTime.Now.AddHours(-1) };
        var result = await _service.ReserveAsync(form, table, _user);
        Assert.That(result, Is.True, "Reservation succeeded");
    }

    [Test]
    public async Task ReserveAsync_OverlappingReservation_Throws()
    {
        var user = new User() { Email = "a@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 10 };
        var user2 = new User() { Email = "a2@b.com", Feedbacks = new List<Feedback>(), FirstName = "a", LastName = "b", Password = "123", Reservations = new List<Reservation>(), Restaurants = new List<Restaurant>(), Role = "USER", UserId = 12 };

        var rest = new Restaurant { RestaurantId = 10, Name = "Rest", User = user, UserId = user.UserId };
        var table = new Table { TableId = 10, Restaurant = rest, RestaurantId = rest.RestaurantId, Capacity = 4 };
        var now = DateTime.Now;

        inMemoryDBContext.Users.Add(user);
        inMemoryDBContext.Users.Add(user2);

        inMemoryDBContext.Restaurants.Add(rest);
        inMemoryDBContext.Tables.Add(table);
        inMemoryDBContext.Reservations.Add(new Reservation
        {
            ReservationId = 10,
            Table = table,
            TableId = table.TableId,
            User = user,
            UserId = user.UserId,
            StartTime = now,
            EndTime = now.AddMinutes(30)
        });
        await inMemoryDBContext.SaveChangesAsync();

        var form = new ReservationFormModel
        {
            StartTime = now.AddMinutes(15),
            EndTime = now.AddMinutes(45)
        };

        var form2 = new ReservationFormModel
        {
            StartTime = now.AddMinutes(35),
            EndTime = now.AddMinutes(145)
        };

        await _service.ReserveAsync(form, table, user);

        var ex = Assert.ThrowsAsync<BadHttpRequestException>(() => _service.ReserveAsync(form2, table, user2));
        Assert.That(ex.Message, Is.EqualTo("Der Tisch ist in diesem Zeitraum bereits reserviert."));
    }

}
