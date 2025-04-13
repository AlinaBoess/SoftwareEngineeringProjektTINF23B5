using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class Restaurant
    {
        [Column("restaurantId")]
        public int RestaurantId { get; set; }

        [Column("userId")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("address")]
        public string? Address { get; set; }

        [Column("openingHours")]
        public string? OpeningHours { get; set; }

        [Column("website")]
        public string? Website { get; set; }
        
        [ForeignKey("userId")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
        
    }
}
