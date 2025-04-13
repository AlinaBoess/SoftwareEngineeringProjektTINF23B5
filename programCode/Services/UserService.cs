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

        public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor = null)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

            user.Password = HashPasswordForRegistration(user.Password);
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
            return (user != null && isCorrectPassword(password, user))? user : null;
        }


        
        /// <summary>
        /// Checks if the provided password matches the password of the current User object.
        /// Call this when processing login attempts.
        /// </summary>
        public bool isCorrectPassword(string password, User user)
        {
            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            string providedPasswordHash = Convert.ToHexString(result).ToLower();

            //Note: Strings are primitive data types in C#, therefore the '==' operator will compare the
            //actual values in them against each other, instead of the references of the two string objects
            return providedPasswordHash == user.Password;
        }

        public string HashPasswordForRegistration(string password)
        {
            // Schritt 1: Konvertiere das Passwort in ein Byte-Array
            byte[] rawPassword = System.Text.Encoding.UTF8.GetBytes(password);

            // Schritt 2: Berechne den SHA-256-Hash des Passworts
            byte[] hashBytes = System.Security.Cryptography.SHA256.HashData(rawPassword);

            // Schritt 3: Konvertiere den Hash in eine Hexadezimalzeichenkette
            string passwordHash = Convert.ToHexString(hashBytes).ToLower();

            // Rückgabe des Hashs
            return passwordHash;
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
