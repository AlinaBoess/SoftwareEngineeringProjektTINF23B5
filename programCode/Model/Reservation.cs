namespace RestaurantReservierung.Model
{
    public class Reservation
    {
        User creator;
        Table table;
        long startTime;
        long endTime;

        public Reservation(User creator, Table table, DateTime startTime, DateTime endTime)
        {
            this.creator = creator;
            this.table = table;

            //convert DateTime objects to ulongs for easier checks
            this.startTime = new DateTimeOffset(startTime.ToUniversalTime()).ToUnixTimeSeconds();
            this.endTime = new DateTimeOffset(endTime.ToUniversalTime()).ToUnixTimeSeconds();
        }


        #region Getters / Setters

        public User Creator
        {
            get { return creator; }
        }

        public Table Table
        {
            get { return table; }
            //used to assign an available table to reservation by ReservationSystem
            set { table = value; }
        }

        public long StartTime
        {
            get { return startTime; }
        }

        public long EndTime
        {
            get { return endTime; }
        }

        #endregion
    }
}
