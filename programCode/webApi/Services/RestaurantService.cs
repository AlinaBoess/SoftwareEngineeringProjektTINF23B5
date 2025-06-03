using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class RestaurantService
    {
        private readonly AppDbContext _context;

        public RestaurantService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Restaurant> AddRestaurantAsync(RestaurantFormModel restaurantModel, User user)
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
                        return restaurant;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateRestaurantAsync(Restaurant restaurant, RestaurantFormModel restaurantModel)
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

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId  == id);

            if (restaurant != null)
            {
                return restaurant;
            }
            return null;
        }

        public async Task<bool> DeleteRestaurantAsync(Restaurant restaurant)
        {
            await DeleteImageByRestaurantIdAsync(restaurant.RestaurantId);
            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() > 0) {  
                return true; 
            }
            return false;
        }

        public async Task<List<Restaurant>> GetManyRestaurantsAsync(GetManyRestaurantFormModel model)
        {
            var query = _context.Restaurants.AsQueryable();         
           
            if(model.name != null)
                query = query.Where(r => r.Name.Contains(model.name));

            query = query.Skip(model.start);
            if (model.count > 0)
                query = query.Take(model.count);

            return await query.ToListAsync();
        }

        public async Task<List<Restaurant>> GetUserRestaurantsAsync(User user)
        {
            return await _context.Restaurants.Where(r => r.User == user).ToListAsync();
        }

        public async Task<bool> OwnsRestaurantAsync(User user, int restaurantId)
        {
            var restaurant = await _context.Restaurants
                .Where(r => r.RestaurantId == restaurantId)
                .Where(r => r.User == user)
                .FirstOrDefaultAsync();

            return restaurant != null;
        }

        public async Task<bool> UploadImageAsync(int restaurantId, IFormFile picture)
        {

            using var memoryStream = new MemoryStream();
            await picture.CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();

            var restaurant = await GetRestaurantByIdAsync(restaurantId);

            if (restaurant == null) return false;

            if (restaurant.ImageId != null)
            {
                restaurant.Image.Data = imageBytes;
                restaurant.Image.MimeType = picture.ContentType;
            }
            else
            {
                var image = new Image
                {
                    MimeType = picture.ContentType,
                    Data = imageBytes
                };
                restaurant.Image = image;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        
        public async Task<Image> GetImageByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null) return null;

            return await _context.Images.FirstOrDefaultAsync(i => i.ImageId == restaurant.ImageId);
        }

        public async Task<bool> DeleteImageByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);

            if (restaurant.ImageId == null)
                return true;   

            _context.Images.Remove(restaurant.Image);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
