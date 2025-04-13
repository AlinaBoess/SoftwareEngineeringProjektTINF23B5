using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class Table
    {
        [Column("tableId")]
        public int TableId { get; set; }

        [Column("restaurantId")]
        public int RestaurantId { get; set; }

        [Column("tableNr")]
        public int? TableNr { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("area")]
        public string? Area { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        [ForeignKey("restaurantId")]
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
