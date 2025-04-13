using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class Feedback
    {
        [Column("feedbackId")]
        public int FeedbackId { get; set; }

        [Column("userId")]
        public int UserId { get; set; }

        [Column("reservationId")]
        public int ReservationId { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("reservationId")]
        public virtual Reservation Reservation { get; set; } = null!;

        [ForeignKey("userId")]
        public virtual User User { get; set; } = null!;
    }
}
