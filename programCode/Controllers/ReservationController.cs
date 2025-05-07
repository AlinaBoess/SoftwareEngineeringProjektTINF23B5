using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly RestaurantOwnerService _restaurantOwnerService;

        public ReservationController(ReservationSystem reservationSystem, UserService userService, TableService tableService, RestaurantOwnerService restaurantOwnerService)
        {
            // Initialisiere das ReservationSystem im Konstruktor
            _reservationSystem = reservationSystem;
            _userService = userService;
            _tableService = tableService;
            _restaurantOwnerService = restaurantOwnerService;
        }

        /// <summary>
        /// Make A Reservation for a table.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
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
        /// Get all Reservations, filtered by query parameters. Only Admins can Access.
        /// 
        /// Filtering behavior of startDate and endDate:
        /// 1. Only `startDate` is set: Returns all reservations that start on or after the given `startDate`.
        /// 2. Only `endDate` is set: Returns all reservations that end on or before the given `endDate`.
        /// 3. Both `startDate` and `endDate` are set: Returns all reservations that overlap with the given date range.
        ///
        /// </summary>
        /// <returns>List of Rerservations</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("All")] 
        public async Task<ActionResult> GetAllReservations([FromQuery] ReservationReturnFormModel model)
        {         
            return Ok(ReservationDto.MapToDtos(await _reservationSystem.GetReservations(model)));
        }

        /// <summary>
        /// Retrieves all reservations for the restaurants owned by the authenticated restaurant owner.
        /// Only users with the role RESTAURANT_OWNER can access this endpoint.
        ///
        /// Filtering behavior of startDate and endDate:
        /// 1. Only `startDate` is set: Returns all reservations that start on or after the given `startDate`.
        /// 2. Only `endDate` is set: Returns all reservations that end on or before the given `endDate`.
        /// 3. Both `startDate` and `endDate` are set: Returns all reservations that overlap with the given date range.
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of Reservations</returns>
        [Authorize(Roles = "RESTAURANT_OWNER")]
        [HttpGet("Owner")] 
        public async Task<ActionResult> GetAllReservationsForOwner([FromQuery] ReservationReturnFormModel model)
        {
            var user = await _userService.GetLoggedInUser();

            if (model.RestaurantId.HasValue)
            {
                if (await _restaurantOwnerService.OwnsRestaurant(user, (int)model.RestaurantId))
                {
                    return Ok(ReservationDto.MapToDtos(await _reservationSystem.GetReservations(model)));
                }
                else
                {
                    return Unauthorized(new { Message = "You are not the owner of the restaurant." });
                }
            }
            else
            {           
                var restaurants = await _restaurantOwnerService.GetUserRestaurants(user);

                return Ok(ReservationDto.MapToDtos(await _reservationSystem.GetReservationsForRestaurants(restaurants, model)));

            }
        }

        /// <summary>
        /// Get All Reservations made from the user who is logged in. 
        /// 
        ///
        /// Filtering behavior of startDate and endDate:
        /// 1. Only `startDate` is set: Returns all reservations that start on or after the given `startDate`.
        /// 2. Only `endDate` is set: Returns all reservations that end on or before the given `endDate`.
        /// 3. Both `startDate` and `endDate` are set: Returns all reservations that overlap with the given date range.
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> GetReservationsForUser([FromQuery] ReservationReturnFormModel model)
        {
            model.UserId = (await _userService.GetLoggedInUser()).UserId;

            return Ok(ReservationDto.MapToDtos(await _reservationSystem.GetReservations(model)));
        }

        [Authorize]
        [HttpPut("{reservationId}")]
        public async Task<IActionResult> UpdateReservation(int reservationId, ReservationFormModel model)
        {
            var user = await _userService.GetLoggedInUser();
            var reservation = await _reservationSystem.GetReservationById(reservationId);


            if(user.UserId != reservation.UserId && user.Role != "ADMIN")
                return Unauthorized(new { Message = "You don't have Permissions to change the reservation"});

            if (model.EndTime <= model.StartTime)
                return BadRequest(new { Message = "Illegal time interval!" });

            if (!_reservationSystem.IsGreaterThenMinTimeInterval(model))
                return BadRequest(new { Message = "The reservation time interval has to be >= " + _reservationSystem.MinReservationTime.ToString() + "." });

            if (_reservationSystem.IsInPast(model))
                return BadRequest(new { Message = "The given Time interval is in the past!" });

            if (! await _reservationSystem.CanUpdateReservation(reservation, model))
                return BadRequest(new { Message = "There already exists a reservation in the given time interval!" });


            if (await _reservationSystem.UpdateReservation(model, reservation))
                return Ok(new { Message = "The reservation was successfull!" });

            return BadRequest(new { Message = "Reservation was not successfull!" });

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

        public int? ReservationId { get; set; }

    }
}