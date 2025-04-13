using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservierung.Models
{
    public partial class AdminAction
    {
        [Column("eventid")]
        public int EventId { get; set; }

        [Column("adminid")]
        public int AdminId { get; set; }

        [Column("actiontype")]
        public string ActionType { get; set; } = null!;

        [Column("actiondescription")]
        public string? ActionDescription { get; set; }

        [Column("actionperformed")]
        public DateTime? ActionPerformed { get; set; }

        [ForeignKey("adminId")]
        public virtual User Admin { get; set; } = null!;
    }
}
