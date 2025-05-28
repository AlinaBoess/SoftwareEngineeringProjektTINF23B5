using Microsoft.IdentityModel.Tokens;
using RestaurantReservierung.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservierung.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // generates the authorizatin token
        public string GenerateJwtToken(User user)
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

        /// <summary>
        /// Checks if the provided password matches the password of the current User object.
        /// Call this when processing login attempts.
        /// </summary>
        public bool IsCorrectPassword(string password, User user)
        {
            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            string providedPasswordHash = Convert.ToHexString(result).ToLower();

            //Note: Strings are primitive data types in C#, therefore the '==' operator will compare the
            //actual values in them against each other, instead of the references of the two string objects
            return providedPasswordHash == user.Password;
        }

        public string HashPasswordForRegistration(string password)
        {
            byte[] rawPassword = System.Text.Encoding.UTF8.GetBytes(password);
        
            byte[] hashBytes = System.Security.Cryptography.SHA256.HashData(rawPassword);

            string passwordHash = Convert.ToHexString(hashBytes).ToLower();

            return passwordHash;
        }
    }
}

