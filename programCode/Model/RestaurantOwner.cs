namespace RestaurantReservierung
{
    public class RestaurantOwner : UserBase
    {
        List<Restaurant> ownedRestaurants = new List<Restaurant>();

        //Ctor
        public RestaurantOwner(string firstName, string lastName, string email, string password) : base(firstName, lastName, email, password)
        {

        }


        /// <summary>
        /// Add room to list of rooms if it is not contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddRoom(ref Restaurant restaurant, Room r)
        {
            if (restaurant == null || restaurant.Rooms == null || r == null || restaurant.Rooms.Contains(r))
                return false;

            restaurant.Rooms.Add(r);
            return true;
        }

        /// <summary>
        /// Remove room to list of rooms if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveRoom(ref Restaurant restaurant, Room r)
        {
            if (restaurant == null || restaurant.Rooms == null || r == null)
                return false;

            return restaurant.Rooms.Remove(r);
        }


        /// <summary>
        /// Add room to list of rooms if it is not contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddTable(ref Room room, Table t)
        {
            if (room == null || room.Tables == null || t == null || room.Tables.Contains(t))
                return false;

            room.Tables.Add(t);
            return true;
        }

        /// <summary>
        /// Remove room to list of rooms if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveTable(ref Room room, Table t)
        {
            if (room == null || room.Tables == null || t == null)
                return false;

            return room.Tables.Remove(t);
        }
    }
}
