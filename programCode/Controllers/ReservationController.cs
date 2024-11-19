using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Services;
using System.Collections.Immutable;

namespace RestaurantReservierung.Controllers
{
    [Authorize]  
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationSystem _reservationSystem;
       

        public ReservationController(ReservationSystem reservationSystem)
        {
            // Initialisiere das ReservationSystem im Konstruktor
            _reservationSystem = reservationSystem;
            
        }


        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {

            List<Room> roomList = new List<Room>();
            List<Table> tableList = new List<Table>();

            tableList.Add(new Table(12, TableAttributes.SQUARE, 1));
            tableList.Add(new Table(12, TableAttributes.SQUARE, 2));
            tableList.Add(new Table(12, TableAttributes.SQUARE, 3));
            tableList.Add(new Table(12, TableAttributes.SQUARE, 4));
            tableList.Add(new Table(4, TableAttributes.ROUND, 5));
            roomList.Add(new Room(100, tableList));
           

            List<Room> roomList2 = new List<Room>();
            List<Table> tableList2 = new List<Table>();

            tableList2.Add(new Table(4, TableAttributes.SQUARE, 1));
            tableList2.Add(new Table(4, TableAttributes.SQUARE, 2));
            roomList2.Add(new Room(100, tableList2));

            List<Room> roomList3 = new List<Room>();
            List<Table> tableList3 = new List<Table>();

            tableList3.Add(new Table(8, TableAttributes.SQUARE, 1));
            tableList3.Add(new Table(8, TableAttributes.SQUARE, 2));
            tableList3.Add(new Table(4, TableAttributes.ROUND, 3));

            roomList3.Add(new Room(100, tableList3));
            roomList.Add(new Room(200, tableList2));


            _reservationSystem.AddRestaurant(new Restaurant("DHBW Kantine", "da bei Ardenauer Ring", new RestaurantOwner("Markus", "Strand", "markus.strand@dhbw-karlsruhe.de", "1234"), roomList));
            _reservationSystem.AddRestaurant(new Restaurant("Mid Dönerladen neben DHBW", "nähe Kindergarten", new RestaurantOwner("Habibi", "Hammud", "hamud.habibi@mail.de", "adfsadf"), roomList2));     
            _reservationSystem.AddRestaurant(new Restaurant("Mr. Meal", "Gegenüber von KFC", new RestaurantOwner("MR", "Meal", "mr.meal@dönerladen.de", "ppp"), roomList3));

            return Ok(_reservationSystem.Restaurants);
        }
    }
}
