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

        public virtual List<TableDto> Tables { get; set; } = new List<TableDto>();

        public virtual UserDto User { get; set; } = null!;

        
        public static RestaurantDto MapToDto(Restaurant restaurant)
        {
            
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
        
    }
}