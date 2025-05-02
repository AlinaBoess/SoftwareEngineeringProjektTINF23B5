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

        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();

        public virtual User User { get; set; } = null!;

        public RestaurantDto MapToDto(Restaurant restaurant)
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
                    Id = t.Id,
                    Seats = t.Seats
                }).ToList(),
            };
        }
    }
}