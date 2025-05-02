using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class ReservationSystem
    {
        //already initialized this way, therefore no extra constructor is needed
        List<Restaurant> restaurants = new List<Restaurant>();
        List<User> users = new List<User>();

        private readonly AppDbContext _context;

        public ReservationSystem(AppDbContext context)
        {
            _context = context;
        }

        public ReservationSystem()
        {
         
        }

        /// <summary>
        /// Adds a restaurant to the reservation system if it is not already contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddRestaurant(Restaurant r)
        {
            if (restaurants == null || r == null || restaurants.Contains(r))
                return false;

            restaurants.Add(r);
            return true;
        }

        /// <summary>
        /// Removes a restaurant from the reservation system if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveRestaurant(Restaurant r)
        {
            if (restaurants == null || r == null)
                return false;

            return restaurants.Remove(r);
        }

        /// <summary>
        /// Adds a restaurant to the reservation system if it is not already contained.
        /// Returns true if successful.
        /// </summary>
        public bool AddUser(User user)
        {
            if (users == null || user == null || users.Contains(user))
                return false;

            users.Add(user);
            return true;
        }

        /// <summary>
        /// Removes a restaurant from the reservation system if it is contained.
        /// Returns true if successful.
        /// </summary>
        public bool RemoveUser(User user)
        {
            if (users == null || user == null)
                return false;

            return users.Remove(user);
        }

        public async Task<bool> Reserve(ReservationFormModel model, Table table, User user)
        {
            var otherReservation = await _context.Reservations
                .Where(r => r.TableId == table.TableId && (
                    (r.StartTime >= model.StartTime && r.StartTime <= model.EndTime) ||
                    (r.EndTime >= model.StartTime && r.EndTime <= model.EndTime) ||
                    (r.StartTime <= model.StartTime && r.EndTime >= model.EndTime)
                 )).ToListAsync();

            Console.WriteLine("Other Restaurant: " + otherReservation.Count);

            if(otherReservation.Count == 0)
            {
                var reservation = new Reservation
                {
                    User = user,
                    Table = table,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                };
                _context.Reservations.Add(reservation);
                if (await _context.SaveChangesAsync() > 0) return true;
            }
                

            return false;
        }

        #region Getters / Setters

        public List<Restaurant> Restaurants
        {
            get { return restaurants; }
        }

        public List<User> Users
        {
            get { return users; }
        }

        #endregion
    }
}
