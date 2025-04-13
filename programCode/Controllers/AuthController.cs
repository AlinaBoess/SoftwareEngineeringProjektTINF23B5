using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        // registers a new user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var user = new User { 
                FirstName = model.FirstName, 
                LastName = model.LastName, 
                Email = model.Email, 
                Password = model.Password 
            };


            if (await _userService.RegisterAsync(user))
                return Ok(new { message = "User registered successfully" });

            return BadRequest(new { message = "User already exists" });
        }

        // logs a new user in and sets a oidc token
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
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // generates the authorizatin token
        private string GenerateJwtToken(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (user.Role == "ADMIN")
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "ADMIN"));
            }
            else if (user.Role == "RESTAURANT_OWNER")
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "RESTAURANT_OWNER"));
            }


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
