using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        /// <summary>
        /// Register a new User. 
        /// The Email has to be valid and unique email. 
        /// The Password has to be at least 6 characters.
        /// A successfull registration returns the jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };


            if (await _userService.RegisterAsync(user))
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(new { message = "User registered successfully.", token });
            }
            return BadRequest(new { message = "User could not be registered!" });
        }

        /// <summary>
        /// Login User. Return userinfo and token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input" });
            }

            // validate user
            User user = await _userService.ValidateUserAsync(model.Email, model.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // generate jwt-token
            var token = _authService.GenerateJwtToken(user);

            return Ok(new { token,  user = UserDto.MapToDto(user) });
        }



        // represents the login form in the frontend
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // represents the register input form in the frontend
        public class RegisterModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            
        }
    }
}