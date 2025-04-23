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

        /// <summary>
        /// Creates a new restaurant. Only a user of Role restaurant_owner or an admin can delete a restaurant.
        /// </summary>
        /// <param name="restaurantModel"></param>
        /// <returns>Status</returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPost]
        public async Task<IActionResult> createRestaurant([FromBody] RestaurantFormModel restaurantModel)
        {
            var user = await _userService.GetLoggedInUser();
            if(user == null)
            {
                return BadRequest();
            }

            if (await _ownerService.AddRestaurant(restaurantModel, user))
            {
                return Ok(new { Message = "The restaurant has been created successfully" });
            }
            return BadRequest(new { Message = "Restaurant could not be created"});

        }

        /// <summary>
        /// Updates a Rerstaurant by id. Only the restaurant owner or an admin can delete a restaurant.
        /// </summary>
        /// <param name="restaurantModel"></param>
        /// <param name="id"></param>
        /// <returns>Status</returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantFormModel restaurantModel, int id)
        {
            var user = await _userService.GetLoggedInUser();
            if(user == null)
            {
                return BadRequest();
            }

            var restaurant = await _ownerService.GetRestaurantById(id);
            if(restaurant == null)
            {
                return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });
            }


            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                if (await _ownerService.UpdateRestaurant(restaurant, restaurantModel))
                {
                    return Ok(new { Message = "The restaurant has been updated successfully" });
                }
                return BadRequest(new { Message = "Restaurant could not be updated" });
            }
            else
            {
                return Unauthorized(new { Message = "You are not the owner of this Restaurant!"});
            }
            
        }

        /// <summary>
        /// Deletes a restaurant by id. Only the restaurant owner or an admin can delete a restaurant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var user = await _userService.GetLoggedInUser();
            if (user == null)
            {
                return BadRequest();
            }

            var restaurant = await _ownerService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });
            }


            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                if (await _ownerService.DeleteRestaurant(restaurant))
                {
                    return Ok(new { Message = "The restaurant has been deleted successfully" });
                }
                return BadRequest(new { Message = "Restaurant could not be deleted" });
            }
            else
            {
                return Unauthorized(new { Message = "You are not the owner of this Restaurant!" });
            }
        }


        /// <summary>
        /// Get Restaurants
        /// </summary>
        /// <returns>A List of All Restaurants</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _ownerService.GetManyRestaurants();

            return Ok(restaurants);
        }

        /// <summary>
        /// Get one Restaurant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One Restaurant</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurants(int id)
        {
            var restaurant = await _ownerService.GetRestaurantById(id);
            if(restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });

        }

        /// <summary>
        /// get the First {count} restaurants.
        /// </summary>
        /// <param name="count">The first X</param>
        /// <returns>A List of Restaurants</returns>
        [HttpGet("many/{count}")]
        public async Task<IActionResult> GetManyRestaurants(int count)
        {
            var restaurants = await _ownerService.GetManyRestaurants(count);

            return Ok(restaurants);
        }

    }

    

    public class RestaurantFormModel
    {
        [Required]
        public string Name { get; set; }
        public string Adress { get; set; }
        public string OpeningHours { get; set; }

        public string Website {  get; set; }
    }
}
