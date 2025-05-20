using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Dtos;
using RestaurantReservierung.Models;
using System;

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
                    Pictures = Convert.FromBase64String(restaurantModel.Pictures.Substring(restaurantModel.Pictures.IndexOf(",") + 1)),
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
            restaurant.Pictures = Convert.FromBase64String(restaurantModel.Pictures.Substring(restaurantModel.Pictures.IndexOf(",") + 1));

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
            var query = _context.Restaurants.AsQueryable();         
           
            query = query.Skip(start);
            if (count > 0)
                query = query.Take(count);

            return await query.ToListAsync();
        }

        public async Task<List<Restaurant>> GetUserRestaurants(User user)
        {
            return await _context.Restaurants.Where(r => r.User == user).ToListAsync();
        }

        public async Task<bool> OwnsRestaurant(User user, int restaurantId)
        {
            var restaurant = await _context.Restaurants
                .Where(r => r.RestaurantId == restaurantId)
                .Where(r => r.User == user)
                .FirstAsync();

            return restaurant != null;
        }
    }
}
