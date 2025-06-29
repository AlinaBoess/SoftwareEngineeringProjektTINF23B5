using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace RestaurantReservierung.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly List<User> _users = new List<User>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;

        public UserService(AppDbContext context, AuthService authService, IHttpContextAccessor httpContextAccessor = null)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> RegisterAsync(User user)
        {
            string emailRegex = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            string passwordRegex = "^.{6,64}$";

            // Prüfen, ob ein Benutzer mit dieser E-Mail bereits existiert
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new BadHttpRequestException("There already exists an user with the given email");

            if (!Regex.IsMatch(user.Email, emailRegex))
                throw new BadHttpRequestException("The given email is not in a valid format");

            if (!Regex.IsMatch(user.Password, passwordRegex))
                throw new BadHttpRequestException("The password is shorter than 6 or longer than 64 characters");

            if (user.Email.Length > 255)
                throw new BadHttpRequestException("The given email is too long");

            user.Email = user.Email.ToLower();
            user.Password = _authService.HashPasswordForRegistration(user.Password);

            // if no users exist, the first user is an admin
            if (!await _context.Users.AnyAsync())
                user.Role = "ADMIN";

            // Benutzer hinzufügen und speichern
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> getUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            return (user != null && _authService.IsCorrectPassword(password, user))? user : null;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRoleAsync(User user, string newRole)
        {
            try
            {
                user.Role = newRole;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetLoggedInUserAsync()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine(email);
            if (email != null)
            {
                return await GetUserByEmailAsync(email);
            }
            return null;
        }

    }
}
