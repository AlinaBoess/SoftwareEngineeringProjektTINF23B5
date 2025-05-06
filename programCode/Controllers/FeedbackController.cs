using Microsoft.AspNetCore.Mvc;

namespace RestaurantReservierung.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
