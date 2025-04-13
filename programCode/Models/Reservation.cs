using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class Reservation
    {
        [Column("reservationId")]
        public int ReservationId { get; set; }

        [Column("userId")]
        public int UserId { get; set; }

        [Column("tableId")]
        public int TableId { get; set; }

        [Column("startTime")]
        public DateTime StartTime { get; set; }

        [Column("endTime")]
        public DateTime EndTime { get; set; }

        [Column("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        [ForeignKey("tableId")]
        public virtual Table Table { get; set; } = null!;

        [ForeignKey("userId")]
        public virtual User User { get; set; } = null!;
    }
}
