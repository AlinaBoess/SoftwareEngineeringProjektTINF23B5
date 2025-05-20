using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Services;


namespace RestaurantReservierung.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;
        private readonly UserService _userService;
        private readonly ReservationSystem _reservationSystem;

        public FeedbackController(FeedbackService feedbackService, UserService userService, ReservationSystem reservationSystem)
        {
            _feedbackService = feedbackService;
            _userService = userService;
            _reservationSystem = reservationSystem;
        }

        [Authorize]
        [HttpPost("{reservationId}")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackFormModel model, int reservationId)
        {
            var user = await _userService.GetLoggedInUser();

            var reservation = await _reservationSystem.GetReservationById(reservationId);

            if (reservation == null)
                return Unauthorized( new { Message = "You have not made a reservation at this restaurant, therefore you cant't give feedback!"});

            if (await _feedbackService.CreateFeedback(user, model, reservation))
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
    }
    
    public class FeedbackFormModel
    {
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
