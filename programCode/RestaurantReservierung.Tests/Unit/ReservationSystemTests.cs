using NUnit.Framework;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Tests.Unit;

[TestFixture]
public class ReservationSystemTests
{
    private ReservationSystem _system;
    private Restaurant _restaurant;

    [SetUp]
    public void SetUp()
    {
        // fresh instance before each test
        _system = new ReservationSystem();

        // a minimal Restaurant instance
        _restaurant = new Restaurant();
        _restaurant.Name = "TestName";
        _restaurant.Address = "TestAddress";
        _restaurant.User = new User();
        _restaurant.Tables = new List<Table>();
    }

    [TearDown]
    public void TearDown()
    {
        // clean up references
        _system = null;
        _restaurant = null;
    }

    [Test]
    public void AddRestaurant_WithValidRestaurant_ReturnsTrueAndContainsIt()
    {
        var added = _system.AddRestaurant(_restaurant);

        Assert.That(added, Is.True, "AddRestaurant should return true for a new restaurant");
        Assert.That(_system.Restaurants, Does.Contain(_restaurant));
    }

    [Test]
    public void AddRestaurant_Duplicate_ReturnsFalse()
    {
        _system.AddRestaurant(_restaurant);
        var addedAgain = _system.AddRestaurant(_restaurant);

        Assert.That(addedAgain, Is.False, "Should prevent adding the same restaurant twice");
        Assert.That(_system.Restaurants.Count, Is.EqualTo(1), "List count should remain 1");
    }

    [Test]
    public void AddRestaurant_Null_ReturnsFalse()
    {
        Assert.That(_system.AddRestaurant(null), Is.False, "Should return false when given null");
        Assert.That(_system.Restaurants, Is.Empty, "List should stay empty");
    }

    [Test]
    public void RemoveRestaurant_Existing_ReturnsTrueAndRemovesIt()
    {
        _system.AddRestaurant(_restaurant);
        var removed = _system.RemoveRestaurant(_restaurant);

        Assert.That(removed, Is.True, "Should return true when removing an existing restaurant");
        Assert.That(_system.Restaurants, Does.Not.Contain(_restaurant), "Should no longer contain that restaurant");
    }

    [Test]
    public void RemoveRestaurant_NotExisting_ReturnsFalse()
    {
        var other = new Restaurant { Name = "Other", Address = "Addr", User = new User(), Tables = new List<Table>() };
        var removed = _system.RemoveRestaurant(other);

        Assert.That(removed, Is.False, "Should return false when trying to remove one never added");
        Assert.That(_system.Restaurants, Is.Empty, "Original list should remain unchanged");
    }

}
