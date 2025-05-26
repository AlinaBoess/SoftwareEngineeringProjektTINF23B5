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
        private readonly ReservationSystem _reservationSystem;
        private readonly RestaurantOwnerService _ownerService;

        public FeedbackController(FeedbackService feedbackService, UserService userService, ReservationSystem reservationSystem, RestaurantOwnerService ownerService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
            _reservationSystem = reservationSystem;
            _ownerService = ownerService;
        }

        [Authorize]
        [HttpPost("{restaurantId}/{reservationId}")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackFormModel model, int restaurantId, int reservationId)
        {
            var user = await _userService.GetLoggedInUser();

            var reservation = await _reservationSystem.GetReservationById(reservationId);

            var restaurant = await _ownerService.GetRestaurantById(restaurantId);

            if (model.Rating > 5 || model.Rating < 0)
                return BadRequest(new { Message = "Illegal Rating Interval!" });

            if(restaurant == null)
                return NotFound(new { Message = $"The restaurant with the id {restaurantId} does not exist!"});

            if (reservation == null)
                return Unauthorized( new { Message = "You have not made a reservation at this restaurant, therefore you cant't give feedback!"});

            if (await _feedbackService.CreateFeedback(user, model, restaurant, reservation))
                return Ok("The Feedback has been created successfully!");

            return BadRequest( new { Message = "The Feedback could not be created" });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var feedback = await _feedbackService.GetFeedbackById(feedbackId);

            if(feedback == null)
                return BadRequest( new { Message = $"The feedback wit the id '{feedbackId}' does not exist!"});

            if (await _feedbackService.DeleteFeedback(feedback))
                return Ok(new { Message = "The reservation has been deleted successfully!" });

            return BadRequest( new { Message = "The reservation could not be deleted!"});
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetFeedback(int restaurantId)
        {
            var restaurant = await _ownerService.GetRestaurantById(restaurantId);

            if(restaurant == null)
                return BadRequest(new { Message = $"The restaurant wit the id '{restaurantId}' does not exist!" });

            var feedbacks = await _feedbackService.GetFeedbacksForRestaurant(restaurant);

            return Ok(FeedbackDto.MapToDtos(feedbacks));
        }
    }
    
    public class FeedbackFormModel
    {
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
