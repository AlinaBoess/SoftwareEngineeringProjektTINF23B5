namespace RestaurantReservierung
{
    public class Room
    {
        List<Table> tables = new List<Table>();
        int roomNumber;

        public Room()
        {
            
        }
        public Room(int roomNumber, List<Table> tables)
        {
            this.roomNumber = roomNumber;
            this.tables = tables;
        }

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
