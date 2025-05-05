using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;
using System.Collections.Immutable;

namespace RestaurantReservierung.Controllers
{
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
            if (table == null)
                return NotFound(new { Message = "The table does not exist." });


            if (model.EndTime <= model.StartTime)
                return BadRequest(new { Message = "Illegal time interval!" });

            if (!_reservationSystem.IsGreaterThenMinTimeInterval(model))
                return BadRequest(new { Message = "The reservation time interval has to be >= " + _reservationSystem.MinReservationTime.ToString() + "." });

            if (_reservationSystem.IsInPast(model))
                return BadRequest(new { Message = "The given Time interval is in the past!" });

            if ((await _reservationSystem.GetReservationsForTimeInterval(model, table)).Count > 0)
                return BadRequest(new { Message = "There already exists a reservation in the given time interval!" });


            if (await _reservationSystem.Reserve(model, table, user))
                return Ok(new { Message = "The reservation was successfull!" });

            return BadRequest(new { Message = "Reservation was not successfull!" });
        }
        
        /// <summary>
        /// Get Reservations, filtered by query parameters.
        /// </summary>
        /// <returns></returns>
        
        [HttpGet] // TODO: Filtern Nutzerrechte, wer darf welche Reservierungen sehen.
        public async Task<ActionResult> GetAllReservations([FromQuery] ReservationReturnFormModel model)
        {
            var user = await _userService.GetLoggedInUser();

            return Ok(ReservationDto.MapToDtos(await _reservationSystem.GetReservations(model)));
        }


    }
    
    public class ReservationFormModel
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }

    public class ReservationReturnFormModel
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? TableId { get; set; }

        public int? RestaurantId { get; set; }

        public int? Start { get; set; }

        public int? Count { get; set; }

        public int? UserId { get; set; }

    }
}