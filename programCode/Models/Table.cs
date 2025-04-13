using System;
using System.Collections.Generic;

namespace RestaurantReservierung.Models;

public partial class Table
{
    public int TableId { get; set; }

    public int RestaurantId { get; set; }

    public int? TableNr { get; set; }

    public int Capacity { get; set; }

    public string? Area { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
