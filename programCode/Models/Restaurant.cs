using System;
using System.Collections.Generic;

namespace RestaurantReservierung.Models;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? OpeningHours { get; set; }

    public string? Website { get; set; }

    public int? ImageId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Image? Image { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();

    public virtual User User { get; set; } = null!;
}
