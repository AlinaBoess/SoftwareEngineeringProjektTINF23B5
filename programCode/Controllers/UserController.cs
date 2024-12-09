using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservierung.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        /// <summary>
        /// Deletes a User specified by ID. Only Admins can use this Endpoint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletion Status</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id) {
            if (_userService.deleteUser(_userService.getUserById(id)))
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
        public ActionResult DeleteCurrentUser()
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim != null)
            {
                if (_userService.deleteUser(_userService.GetUserByEmail(emailClaim.Value)))
                {
                    return Ok(new { Message = "User has been deleted successfully" });
                }else {
                    return BadRequest("User could not be deleted");
                }
                

            }
            else
            {
                return BadRequest(new {Message = "No User found to delete"});
            }
           
        }

        /// <summary>
        /// Allows Admins to create new Users without the Authentication Process.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Deletion Status</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateUser([FromBody] User model)
        {
            return Ok();
        }

    }
   
}
