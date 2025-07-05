using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly TableService _tableService;
        private readonly RestaurantService _restaurantService;

        public ReservationController(ReservationService reservationSystem, UserService userService, TableService tableService, RestaurantService restaurantOwnerService)
        {
            // Initialisiere das ReservationSystem im Konstruktor
            _reservationService = reservationSystem;
            _userService = userService;
            _tableService = tableService;
            _restaurantService = restaurantOwnerService;
        }

        /// <summary>
        /// Make A Reservation for a table.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{tableId}")]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationFormModel model, int tableId)
        {
            if (await _reservationService.ReserveAsync(model, tableId))
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
        [HttpGet("Admin")] 
        public async Task<ActionResult> GetAllReservations([FromQuery] ReservationFilterModel model)
        {         
            return Ok(ReservationDto.MapToDtos(await _reservationService.GetReservationsAsync(model)));
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
        public async Task<ActionResult> GetAllReservationsForOwner([FromQuery] ReservationFilterModel model)
        {
            var user = await _userService.GetLoggedInUserAsync();

            if (model.RestaurantId.HasValue)
            {
                if (await _restaurantService.OwnsRestaurantAsync(user, (int)model.RestaurantId))
                {
                    return Ok(ReservationDto.MapToDtos(await _reservationService.GetReservationsAsync(model)));
                }
                else
                {
                    return Unauthorized(new { Message = "You are not the owner of the restaurant." });
                }
            }
            else
            {           
                var restaurants = await _restaurantService.GetUserRestaurantsAsync(user);

                return Ok(ReservationDto.MapToDtos(await _reservationService.GetReservationsForRestaurantsAsync(restaurants, model)));

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
        public async Task<IActionResult> GetReservationsForUser([FromQuery] ReservationFilterModel model)
        {
            model.UserId = (await _userService.GetLoggedInUserAsync()).UserId;

            return Ok(ReservationDto.MapToDtos(await _reservationService.GetReservationsAsync(model)));
        }

        /// <summary>
        /// Get All Reservations which does not contain user specific data. 
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
        [HttpGet("Public")]
        public async Task<IActionResult> GetAnonymousReservations([FromQuery] ReservationFilterModel model)
        {
            return Ok(ReservationDto.MapToPublicDtos(await _reservationService.GetReservationsAsync(model)));
        }

        /// <summary>
        /// Update a Reservation by id.
        /// </summary>
        /// <param name="reservationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{reservationId}")]
        public async Task<IActionResult> UpdateReservation(int reservationId, ReservationFormModel model)
        {
            if (await _reservationService.UpdateReservationAsync(model, reservationId))
                return Ok(new { Message = "The reservation was successfull!" });

            return BadRequest(new { Message = "Reservation was not successfull!" });

        }

        /// <summary>
        /// Deletes a Reservation by id.
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {

            if (await _reservationService.DeleteReservationAsync(reservationId))
                return Ok(new { Message = "The Reservation has been canceled successfully!" });

            return BadRequest( new { Message = "The Reservation could not be canceled"});
        } 


    }
    
    public class ReservationFormModel
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }

    public class ReservationFilterModel
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