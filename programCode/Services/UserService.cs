using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;

namespace RestaurantReservierung.Services
{
    public class UserService
    {

        private readonly List<UserBase> _users = new List<UserBase>();

        public UserService() {
            _users.Add(new Admin("markus", "strand", "lal@lel.de", "1234"));
        }

        // Alle Benutzer abrufen
        public IEnumerable<UserBase> GetAllUsers()
        {
            return _users;
        }

        // Benutzer nach E-Mail suchen
        public UserBase GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

       
        // Benutzer registrieren
        public bool Register(UserBase user)
        {
            // Überprüfen, ob die E-Mail bereits existiert
            if (_users.Any(u => u.Email == user.Email))
                return false;

            _users.Add(user);
            return true;
        }


        public bool ValidateUser(string email, string password, out UserBase user)
        {
            user = GetUserByEmail(email);

            return user != null && isCorrectPassword(password, user);
        }


        
        /// <summary>
        /// Checks if the provided password matches the password of the current User object.
        /// Call this when processing login attempts.
        /// </summary>
        public bool isCorrectPassword(string password, UserBase user)
        {
            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            string providedPasswordHash = Convert.ToHexString(result).ToLower();

            //Note: Strings are primitive data types in C#, therefore the '==' operator will compare the
            //actual values in them against each other, instead of the references of the two string objects
            return providedPasswordHash == user.PasswordHash;
        }
        
    }
}
