using Microsoft.AspNetCore.Identity;

namespace RestaurantReservierung
{
    /// <summary>
    /// base interface where all kinds of users derive from
    /// </summary>
    public abstract class UserBase
    {
        //Note: In C#, no keyword in front of a data type means that it is implicitly private
        string firstName;
        string lastName;
        string email;
        string passwordHash;


        /// <summary>
        /// Creates a new user object and securely stores their password.
        /// </summary>
        public UserBase(string firstName, string lastName,  string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;

            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            this.passwordHash = Convert.ToHexString(result).ToLower();
        }

        /// <summary>
        /// Checks if the provided password matches the password of the current User object.
        /// Call this when processing login attempts.
        /// </summary>
        public bool isCorrectPassword(string password)
        {
            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            string providedPasswordHash = Convert.ToHexString(result).ToLower();

            //Note: Strings are primitive data types in C#, therefore the '==' operator will compare the
            //actual values in them against each other, instead of the references of the two string objects
            return providedPasswordHash == passwordHash;
        }


        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; }
        }
    }
}
