using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class RestaurantOwnerService
    {
        private readonly AppDbContext _context;

        public RestaurantOwnerService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant != null)
                {
                    _context.Restaurants.Add(restaurant);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /*
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
        */
    }
}
