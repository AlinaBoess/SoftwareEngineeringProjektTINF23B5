using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;


namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;
        private readonly UserService _userService;
        private readonly ReservationService _reservationService;
        private readonly RestaurantService _restaurantService;

        public FeedbackController(FeedbackService feedbackService, UserService userService, ReservationService reservationSystem, RestaurantService ownerService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
            _reservationService = reservationSystem;
            _restaurantService = ownerService;
        }

        /// <summary>
        /// Create a feedback for a restaurant. The user has to have a valid reservation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{reservationId}")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackFormModel model, int reservationId)
        {
            var user = await _userService.GetLoggedInUserAsync();

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            var restaurant = reservation.Table.Restaurant;

            if (model.Rating > 5 || model.Rating < 1) 
                return BadRequest(new { Message = "Illegal Rating Interval!" });

            if (reservation == null)
                return Unauthorized( new { Message = "You have not made a reservation at this restaurant, therefore you cant't give feedback!"});

            if (await _feedbackService.CreateFeedbackAsync(user, model, restaurant, reservation))
                return Ok(new { Message = "The Feedback has been created successfully!" });

            return BadRequest( new { Message = "The Feedback could not be created" });
        }

        /// <summary>
        /// Delete any feedback. Only Admins use this endpoint
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);

            if(feedback == null)
                return BadRequest( new { Message = $"The feedback wit the id '{feedbackId}' does not exist!"});

            if (await _feedbackService.DeleteFeedbackAsync(feedback))
                return Ok(new { Message = "The reservation has been deleted successfully!" });

            return BadRequest( new { Message = "The reservation could not be deleted!"});
        }

        /// <summary>
        /// Get all Feedbacks for a restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetFeedback(int restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

            if(restaurant == null)
                return BadRequest(new { Message = $"The restaurant wit the id '{restaurantId}' does not exist!" });

            var feedbacks = await _feedbackService.GetFeedbacksForRestaurantAsync(restaurant);

            return Ok(FeedbackDto.MapToDtos(feedbacks));
        }
    }
    
    public class FeedbackFormModel
    {
        public byte Rating { get; set; }

        public string? Comment { get; set; }
    }
}
