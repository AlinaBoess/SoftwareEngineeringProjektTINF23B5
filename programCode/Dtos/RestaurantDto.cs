using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.Json;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Dtos
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? OpeningHours { get; set; }
        public string? Website { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<TableDto> Tables { get; set; } = new List<TableDto>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UserDto User { get; set; } = null!;

        
        public static RestaurantDto MapToDto(Restaurant restaurant)
        {
            if(restaurant != null) { 
            
                return new RestaurantDto
                {
                    RestaurantId = restaurant.RestaurantId,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    OpeningHours = restaurant.OpeningHours,
                    Website = restaurant.Website,
                    Tables = restaurant.Tables?                    
                        .Select(t => new TableDto
                        {
                            TableId = t.TableId,
                            TableNr = t.TableNr,
                            Capacity = t.Capacity,
                            Area = t.Area

                        }).ToList(),
                
                    User = new UserDto
                    {
                        UserId = restaurant.User.UserId,
                        FirstName = restaurant.User.FirstName,
                        LastName = restaurant.User.LastName,
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public static List<RestaurantDto> MapToDtos(List<Restaurant> restaurants)
        {
            var restaurantDtos = new List<RestaurantDto>();

            foreach(var restaurant in restaurants)
            {
                restaurantDtos.Add(MapToDto(restaurant));
            }

            return restaurantDtos;
        }
        
    }
}