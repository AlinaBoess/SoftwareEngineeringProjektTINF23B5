using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;
using System.Collections.Concurrent;
using System.Security.Claims;

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

        // Alle Benutzer abrufen
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }



        // Benutzer nach E-Mail suchen
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }



        // Benutzer registrieren
        public async Task<bool> RegisterAsync(User user)
        {
            // Prüfen, ob ein Benutzer mit dieser E-Mail bereits existiert
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (emailExists)
                return false;

            user.Password = _authService.HashPasswordForRegistration(user.Password);
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

        public async Task<User> GetLoggedInUser()
        {
            // Hier Problem
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
