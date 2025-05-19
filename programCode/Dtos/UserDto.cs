using System.Text.Json.Serialization;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Dtos
{
    public class UserDto
    {
        public int? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastName { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; } = null!;

        public static UserDto MapToDto(User user)
        {
            if (user != null) { 
                return new UserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            else
            {
                return null;
            }
        }

        public static List<UserDto> MapToDtos(List<User> users)
        {
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(MapToDto(user));
            }

            return userDtos;
        }
    }
}
