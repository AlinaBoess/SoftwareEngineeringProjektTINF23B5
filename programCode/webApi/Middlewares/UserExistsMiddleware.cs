using RestaurantReservierung.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace RestaurantReservierung.Middlewares
{
    public class UserExistsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserExistsMiddleware> _logger;

        public UserExistsMiddleware(RequestDelegate next, ILogger<UserExistsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

                    if (emailClaim != null)
                    {
                
                        var userService = context.RequestServices.GetRequiredService<UserService>();
                        var user = await userService.GetUserByEmailAsync(emailClaim.Value);
                        if (user == null)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsJsonAsync(new { Message = "The user in the JWT token does not exist." });
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error parsing JWT token.");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new { Message = "Invalid JWT token." });
                    return;
                }
            }

            await _next(context);
        }
    }
}
