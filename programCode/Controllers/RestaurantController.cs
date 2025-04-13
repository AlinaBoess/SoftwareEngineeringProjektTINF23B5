using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantOwnerService _ownerService;
        
        private readonly UserService _userService;
        public RestaurantController(RestaurantOwnerService ownerService, UserService userService)
        {
            _ownerService = ownerService;
            _userService = userService;
        }

        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPost]
        public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantModel restaurantModel)
        {
            var user = await _userService.GetLoggedInUser();
            Console.WriteLine($"user: {user?.UserId}");
            var restaurant = new Restaurant
            {
                Name = restaurantModel.Name,
                Address = restaurantModel.Adress,
                OpeningHours = restaurantModel.OpeningHours,
                Website = restaurantModel.Website,
                UserId = user.UserId
            };

            Console.WriteLine($"UserId: {restaurant.UserId}");
            Console.WriteLine($"User: {restaurant.User?.Email ?? "null"}");


            if (await _ownerService.AddRestaurant(restaurant))
            {
                return Ok(new { Message = "The restaurant has been created successfully" });
            }
            return BadRequest(new { Message = "Restaurant could not be created"});

        }
    }

    public class CreateRestaurantModel
    {
        [Required]
        public string Name { get; set; }
        public string Adress { get; set; }
        public string OpeningHours { get; set; }

        public string Website {  get; set; }
    }
}
