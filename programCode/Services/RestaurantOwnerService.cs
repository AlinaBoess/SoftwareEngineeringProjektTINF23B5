using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Dtos;
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


        public async Task<bool> AddRestaurant(RestaurantFormModel restaurantModel, User user)
        {
            try
            {
                var restaurant = new Restaurant
                {
                    Name = restaurantModel.Name,
                    Address = restaurantModel.Adress,
                    OpeningHours = restaurantModel.OpeningHours,
                    Website = restaurantModel.Website,
                    User = user
                };

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

        public async Task<bool> UpdateRestaurant(Restaurant restaurant, RestaurantFormModel restaurantModel)
        {
            restaurant.Name = restaurantModel.Name;
            restaurant.Address = restaurantModel.Adress;
            restaurant.OpeningHours = restaurantModel.OpeningHours;
            restaurant.Website = restaurantModel.Website;

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId  == id);

            if (restaurant != null)
            {
                return restaurant;
            }
            return null;
        }

        public async Task<bool> DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() > 0) {  
                return true; 
            }
            return false;
        }

        public async Task<List<Restaurant>> GetManyRestaurants(int start = 0, int count = -1)
        {
            if(count >= 0)
            {
                return await _context.Restaurants
                    .Skip(start)
                    .Take(count)
                    .Include(r => r.User)
                    .ToListAsync();
            }
            else 
            {
                return await _context.Restaurants
                    .Skip(start)
                    .Include(r => r.User)
                    .ToListAsync();
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
