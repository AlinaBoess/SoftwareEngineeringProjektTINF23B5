using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Models;
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
        private readonly UserService _userService;
        private readonly TableService _tableService;

        public ReservationController(ReservationSystem reservationSystem, UserService userService, TableService tableService)
        {
            // Initialisiere das ReservationSystem im Konstruktor
            _reservationSystem = reservationSystem;
            _userService = userService;
            _tableService = tableService;
        }


        [Authorize]
        [HttpPost("{tableId}")]
        public async Task<IActionResult> makeReservation([FromBody] ReservationFormModel model, int tableId)
        {
            var user = await _userService.GetLoggedInUser();

            var table = await _tableService.GetTableById(tableId);
            if(table == null)
            {
                return NotFound();
            }


            if(await _reservationSystem.Reserve(model, table, user)){
                return Ok(new { Message = "The reservation was successfull!"});
            }
            return BadRequest(new { Message = "Reservation was not successfull!"});
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {

            return Ok();
        }
    }

    public class ReservationFormModel
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
