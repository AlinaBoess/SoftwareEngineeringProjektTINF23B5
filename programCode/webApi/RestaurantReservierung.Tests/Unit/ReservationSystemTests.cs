using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Tests.Unit;

[TestFixture]
public class ReservationSystemTests
{
    private ReservationService _system;
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
           .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
           .Options;

        inMemoryDBContext = new AppDbContext(options);

        // fresh instances before each test
        _system = new ReservationService(inMemoryDBContext);


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
        var reservation = _system.GetReservationByIdAsync(1);

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.ReservationId, Is.EqualTo(1));

    }

    [Test]
    public void GetReservationForRestaurants_ShouldReturnExisting()
    {
        var reservation = _system.GetReservationsForRestaurantsAsync(new List<Restaurant>() { _restaurant }, new Controllers.ReservationFilterModel() { RestaurantId = _restaurant.RestaurantId });

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.Count, Is.EqualTo(3));
    }

    [Test]
    public void GetReservationsForTimeInterval_ShouldReturnExisting()
    {
        var reservation = _system.GetReservationsForTimeIntervalAsync(new Controllers.ReservationFormModel() { StartTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)), EndTime = DateTime.Now.Add(TimeSpan.FromMinutes(5)) }, _table);

        Assert.That(reservation, Is.Not.Null, "Returned restaurant should not be null");
        Assert.That(reservation.Result, Is.Not.Null);
        Assert.That(reservation.Result.Count, Is.EqualTo(2));

        for (int i = 0; i < 2; ++i)
            Assert.That(reservation.Result[i].Table, Is.EqualTo(_table));
    }
}
