using System;
using System.Collections.Generic;

namespace RestaurantReservierung.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int ReservationId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int RestaurantId { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
