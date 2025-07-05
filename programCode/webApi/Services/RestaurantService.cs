using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class RestaurantService
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;
        public RestaurantService(AppDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
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

        public async Task<bool> UpdateRestaurantAsync(RestaurantFormModel restaurantModel, int id)
        {
            var user = await _userService.GetLoggedInUserAsync();

            var restaurant = await GetRestaurantByIdAsync(id);

            if (restaurant == null)
            {
                throw new BadHttpRequestException("The Restaurant with the id " + id + " does not exist!" );
            }

            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                restaurant.Name = restaurantModel.Name;
                restaurant.Address = restaurantModel.Adress;
                restaurant.OpeningHours = restaurantModel.OpeningHours;
                restaurant.Website = restaurantModel.Website;

                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new BadHttpRequestException("You are not the owner of this Restaurant!");
            }
           
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

        public async Task<bool> DeleteRestaurantAsync(int restaurantId)
        {
            var user = await _userService.GetLoggedInUserAsync();

            var restaurant = await GetRestaurantByIdAsync(restaurantId) ?? throw new BadHttpRequestException("The Restaurant with the id " + restaurantId + " does not exist!" );
            
            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                await DeleteImageByRestaurantIdAsync(restaurant.RestaurantId);
                _context.Restaurants.Remove(restaurant);

                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new BadHttpRequestException("You are not the owner of this Restaurant!" );
            }
          
        }

        public async Task<List<Restaurant>> GetManyRestaurantsAsync(GetManyRestaurantFormModel model)
        {
            var query = _context.Restaurants.Include(r => r.User).AsQueryable();         
           
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
            var user = await _userService.GetLoggedInUserAsync();

            if (!await OwnsRestaurantAsync(user, restaurantId) && user.Role != "ADMIN")
                throw new BadHttpRequestException("You do not have permission to perform this action");

            if (picture == null || picture.Length == 0)
                throw new BadHttpRequestException("No File was uploaded");

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
            var user = await _userService.GetLoggedInUserAsync();

            if (!await OwnsRestaurantAsync(user, restaurantId) && user.Role != "ADMIN")
                throw new BadHttpRequestException("You are not owner of the restaurant");

            var restaurant = await GetRestaurantByIdAsync(restaurantId);

            if (restaurant.ImageId == null)
                return true;   

            _context.Images.Remove(restaurant.Image);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
