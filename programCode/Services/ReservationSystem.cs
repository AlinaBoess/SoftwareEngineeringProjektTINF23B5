namespace RestaurantReservierung.Services
{
    public class ReservationSystem
    {
        //already initialized this way, therefore no extra constructor is needed
        List<Restaurant> restaurants = new List<Restaurant>();
        List<UserBase> users = new List<UserBase>();
        Admin admin;


        /// <summary>
        /// Adds a restaurant to the reservation system if it is not already contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddRestaurant(Restaurant r)
        {
            if (restaurants == null || r == null || restaurants.Contains(r))
                return false;

            restaurants.Add(r);
            return true;
        }

        /// <summary>
        /// Removes a restaurant from the reservation system if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveRestaurant(Restaurant r)
        {
            if (restaurants == null || r == null)
                return false;

            return restaurants.Remove(r);
        }

        /// <summary>
        /// Adds a restaurant to the reservation system if it is not already contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddUser(UserBase user)
        {
            if (users == null || user == null || users.Contains(user))
                return false;

            users.Add(user);
            return true;
        }

        /// <summary>
        /// Removes a restaurant from the reservation system if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveUser(UserBase user)
        {
            if (users == null || user == null)
                return false;

            return users.Remove(user);
        }



        #region Getters / Setters

        public List<Restaurant> Restaurants
        {
            get { return restaurants; }
        }

        public List<UserBase> Users
        {
            get { return users; }
        }

        #endregion
    }
}
