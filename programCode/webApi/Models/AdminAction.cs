using System;
using System.Collections.Generic;

namespace RestaurantReservierung.Models;

public partial class AdminAction
{
    public int EventId { get; set; }

    public int AdminId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? ActionDescription { get; set; }

    public DateTime? ActionPerformed { get; set; }

    public virtual User Admin { get; set; } = null!;
}
