using System;
using System.Collections.Generic;

namespace RestaurantReservierung.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public byte[] Data { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
