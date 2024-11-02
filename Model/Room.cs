namespace RestaurantReservierung
{
    public class Room
    {
        List<Table> tables = new List<Table>();
        int roomNumber;

        public List<Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
    }
}
