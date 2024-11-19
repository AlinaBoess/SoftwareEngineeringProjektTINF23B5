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
        string password;


        /// <summary>
        /// Creates a new user object and securely stores their password.
        /// </summary>
        public UserBase(string firstName, string lastName,  string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;

            //get raw bytes of password string
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(password);

            //hash password using SHA256
            byte[] result = System.Security.Cryptography.SHA256.HashData(raw);

            //construct string of hashed password for easier matching
            this.passwordHash = Convert.ToHexString(result).ToLower();
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

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


    }
}
