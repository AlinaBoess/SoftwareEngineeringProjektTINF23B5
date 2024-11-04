namespace RestaurantReservierung
{
    /// <summary>
    /// The Restaurant class. The RestaurantOwner can make changes (e.g. add rooms or tables)
    /// </summary>
    public class Restaurant
    {
        string name;
        string address;
        List<Room> rooms = new List<Room>();
        RestaurantOwner owner;


        /// <summary>
        /// Creates a new restaurant with the specified arguments.
        /// </summary>
        public Restaurant(string name, string address, RestaurantOwner owner, List<Room> rooms)
        {
            this.name = name;
            this.address = address;
            this.owner = owner;
            this.rooms = rooms;
        }

        #region Getters / Setters

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public List<Room> Rooms
        {
            get { return rooms; }
        }

        public RestaurantOwner Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        #endregion
    }
}
