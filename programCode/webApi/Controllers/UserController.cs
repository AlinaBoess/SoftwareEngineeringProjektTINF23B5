using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;

        }

        /// <summary>
        /// Returns all Users that exist. Only Admins can use this Endpoint.
        /// </summary>
        /// <returns>All Users</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(UserDto.MapToDtos(users));
        }

        /// <summary>
        /// Get a user by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            return Ok(UserDto.MapToDto(user));
        }

        /// <summary>
        /// Deletes a User specified by ID. Only Admins can use this Endpoint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletion Status</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserById(int id) {
            if (await _userService.DeleteUserAsync(await _userService.getUserByIdAsync(id)))
            {
                return Ok(new {Message = "User deleted successfully"});
            }
            else { 
                return NotFound(new { Message = "User not found" });
            }
        }

        /// <summary>
        /// Deletes the User who is currently logged in.
        /// </summary>
        /// <returns>Deletion Status</returns>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteCurrentUser()
        {
            var user = await _userService.GetLoggedInUserAsync();

            return Ok(await _userService.DeleteUserAsync(user));
        }


        /// <summary>
        /// Updates the Role of an user. Only Admins can use this endpoint
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        [Route("Role")]
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        public async Task<ActionResult> UpdateRole(string email, string newRole)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                if(await _userService.UpdateRoleAsync(user, newRole))
                {
                    return Ok(new {Message = "Role has been updated successfully"});
                }
                return BadRequest(new { Message = "Role could not be updated!" });
            }
            return BadRequest(new { Message = "The user with the email " + email + " does not exist" });
        }

        /// <summary>
        /// Returns the user who is currently loggid in (from the jwt token)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var user = await _userService.GetLoggedInUserAsync();

            return Ok(new { user = UserDto.MapToDto(user) });
        }
    }
  
}
