using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class User
    {
        [Column("userId")]
        public int UserId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("last_name")]  // Korrigiert auf "last_name"
        public string LastName { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("role")]
        public string Role { get; set; } = "CUSTOMER";

        public virtual ICollection<AdminAction> AdminActions { get; set; } = new List<AdminAction>();

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


        [InverseProperty("User")]  // ✅ Zeigt auf die Navigation in Restaurant
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
