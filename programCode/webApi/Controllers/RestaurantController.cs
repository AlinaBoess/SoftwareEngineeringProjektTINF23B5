using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Services;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        
        private readonly UserService _userService;

        private readonly FeedbackService _feedbackService;
        public RestaurantController(RestaurantService ownerService, UserService userService, FeedbackService feedbackService)
        {
            _restaurantService = ownerService;
            _userService = userService;
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Creates a new restaurant. Only a user of Role restaurant_owner or an admin can delete a restaurant.
        /// </summary>
        /// <param name="restaurantModel"></param>
        /// <returns>Status</returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantFormModel restaurantModel)
        {
            var user = await _userService.GetLoggedInUserAsync();
            if(user == null)
            {
                return BadRequest();
            }

            if (await _restaurantService.AddRestaurantAsync(restaurantModel, user))
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
            var user = await _userService.GetLoggedInUserAsync();
            if(user == null)
            {
                return BadRequest();
            }

            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if(restaurant == null)
            {
                return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });
            }


            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                if (await _restaurantService.UpdateRestaurantAsync(restaurant, restaurantModel))
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
            var user = await _userService.GetLoggedInUserAsync();
            if (user == null)
            {
                return BadRequest();
            }

            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });
            }


            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                if (await _restaurantService.DeleteRestaurantAsync(restaurant))
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
        /// Get one Restaurant by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One Restaurant</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurants(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            var restaurantDto = RestaurantDto.MapToDto(restaurant);
            if (restaurantDto != null)
            {
                return Ok(restaurantDto);
            }
            return NotFound(new { Message = "The Restaurant with the id " + id + " does not exist!" });

        }

        /// <summary>
        /// Get many Restaurants. If no Url-Parameters are added, the Endpoint will return all Restaurants. 
        /// </summary>
        /// <param name="count">How many restaurants will be returned.</param>
        /// <param name="start">Starting at:</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetManyRestaurants([FromQuery] int count = -1, [FromQuery] int start = 0)
        {
            var restaurants = await _restaurantService.GetManyRestaurantsAsync(start, count);    

            return Ok(RestaurantDto.MapToDtos(restaurants));
        }
        
        /// <summary>
        /// Returns a list of the restaurants from a restaurant owner.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN,RESTAURANT_OWNER")]
        [HttpGet("owner")]
        public async Task<IActionResult> GetOnwerRestaurants()
        {
            var user = await _userService.GetLoggedInUserAsync();

            var restaurants = await _restaurantService.GetUserRestaurantsAsync(user);

            return Ok(RestaurantDto.MapToDtos(restaurants));
        }

        /// <summary>
        /// Returns the average rating, calculated by all reservations for the restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet("Rating/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantRating(int restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

            if (restaurant == null)
                return NotFound(new { Message = $"The restaurant with the id {restaurantId} does not exist!" });

            var rating = await _feedbackService.CalcRestaurantRatingAsync(restaurant);

            return Ok(new { rating });
        }
        
        /// <summary>
        /// Change the Image of an restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN,RESTAURANT_OWNER")]
        [HttpPut("Image/{restaurantId}")]
        public async Task<IActionResult> ChangeImage(int restaurantId, IFormFile picture)
        {
            var user = await _userService.GetLoggedInUserAsync();

            if(!await _restaurantService.OwnsRestaurantAsync(user, restaurantId))
                return Unauthorized();

            if (picture == null || picture.Length == 0)
                return BadRequest( new { Message = "No File was uploaded"});

            if (await _restaurantService.UploadImageAsync(restaurantId, picture))
                return Ok(new { Message = "The picture has been uploaded successfully!" });

            return BadRequest( new { Message = "The picture could not be uploaded!"});

        }

        /// <summary>
        /// Get the image thumbnail of a restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet("Image/{restaurantId}")]
        public async Task<IActionResult> GetImage(int restaurantId)
        {
            var image = await _restaurantService.GetImageByRestaurantIdAsync(restaurantId);

            if(image == null)
                return NoContent();

            return File(image.Data, image.MimeType);
        }
        
        /// <summary>
        /// Delete a image thumbnail for a restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN,RESTAURANT_OWNER")]
        [HttpDelete("Image/{restaurantId}")]
        public async Task<IActionResult> DeleteImage(int restaurantId)
        {
            var user = await _userService.GetLoggedInUserAsync();

            if (!await _restaurantService.OwnsRestaurantAsync(user, restaurantId))
                return Unauthorized();

            if (await _restaurantService.DeleteImageByRestaurantIdAsync(restaurantId))
                return Ok(new { Message = "The Image has been deleted successfully!"});

            return BadRequest( new { Message = "The Image could not be deleted!"});
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
