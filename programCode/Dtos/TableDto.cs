namespace RestaurantReservierung.Dtos
{
    public class TableDto
    {
        public int TableId { get; set; }

        public int? RestaurantId { get; set; }

        public int? TableNr { get; set; }

        public int Capacity { get; set; }

        public string? Area { get; set; }
    }
}
