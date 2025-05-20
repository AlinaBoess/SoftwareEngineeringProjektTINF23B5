using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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

        // The min Timespan how long a reservation needs to be.
        public TimeSpan MinReservationTime { get; } = new TimeSpan(1, 0, 0);

        public ReservationSystem(AppDbContext context)
        {
            _context = context;
        }

        /*      // exeists because otherwise there is a compile error in the test files
              public ReservationSystem()
              {

              }
        */

        public async Task<bool> Reserve(ReservationFormModel model, Table table, User user)
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

            return false;
        }

        public async Task<List<Reservation>> GetReservationsForTimeInterval(ReservationFormModel model, Table table)
        {
            var reservations = await _context.Reservations
                .Where(r => r.TableId == table.TableId && (
                    (r.StartTime >= model.StartTime && r.StartTime <= model.EndTime) ||
                    (r.EndTime >= model.StartTime && r.EndTime <= model.EndTime) ||
                    (r.StartTime <= model.StartTime && r.EndTime >= model.EndTime)
                 )).ToListAsync();

            return reservations;
        }

        public bool IsGreaterThenMinTimeInterval(ReservationFormModel model)
        {
            TimeSpan timestamp = model.EndTime - model.StartTime;
            return (timestamp.CompareTo(MinReservationTime) >= 0);

        }

        public bool IsInPast(ReservationFormModel model)
        {
            return model.StartTime < DateTime.Now;
        }

        public async Task<List<Reservation>> GetReservations(ReservationFilterModel model)
        {
            var query = _context.Reservations.AsQueryable();



            if (model.TableId.HasValue)

                query = query.Where(r => r.TableId == model.TableId);

            if (model.RestaurantId.HasValue)

                query = query.Where(r => r.Table.RestaurantId == model.RestaurantId);

            if (model.UserId.HasValue)

                query = query.Where(r => r.UserId == model.UserId);


            if (model.ReservationId.HasValue)


                query = query.Where(r => r.ReservationId == model.ReservationId);

            if (model.StartTime.HasValue && !model.EndTime.HasValue)

                query = query.Where(r => r.StartTime >= model.StartTime);

            else if (model.EndTime.HasValue && !model.StartTime.HasValue)

                query = query.Where(r => r.EndTime <= model.EndTime);

            else if (model.StartTime.HasValue && model.EndTime.HasValue)

                query = query.Where(r => (

                    (r.StartTime >= model.StartTime && r.StartTime <= model.EndTime) ||

                    (r.EndTime >= model.StartTime && r.EndTime <= model.EndTime) ||

                    (r.StartTime <= model.StartTime && r.EndTime >= model.EndTime)

                ));

            if (model.Start.HasValue)

                query = query.Skip((int)model.Start);

            if (model.Count.HasValue)

                query = query.Take((int)model.Count);



            var reservations = await query.ToListAsync();

            return reservations;

            /*var query = _context.Reservations.AsQueryable();
            query = query.Where(r => r.TableId == model.TableId)
                         .Where(r => r.Table.RestaurantId == model.RestaurantId)
                         .Where(r => r.UserId == model.UserId)
                         .Where(r => r.ReservationId == model.ReservationId)
                         .Where(r => r.StartTime >= model.StartTime)
                         .Where(r => r.EndTime <= model.EndTime)
                         .Where(r => (
                            (r.StartTime >= model.StartTime && r.StartTime <= model.EndTime) ||
                            (r.EndTime >= model.StartTime && r.EndTime <= model.EndTime) ||
                            (r.StartTime <= model.StartTime && r.EndTime >= model.EndTime)
                         ));

            if (model.Start.HasValue)
                query = query.Skip((int)model.Start);
            if (model.Count.HasValue)
                query = query.Take((int)model.Count);

            var reservations = await query.ToListAsync();
            return reservations; */

        }

        public async Task<List<Reservation>> GetReservationsForRestaurants(List<Restaurant> restaurants, ReservationFilterModel model)
        {
            var reservations = new List<Reservation>();

            foreach (var restaurant in restaurants)
            {
                model.RestaurantId = restaurant.RestaurantId;
                reservations = [.. reservations, .. await GetReservations(model)];
            }
            return reservations;
        }

        public async Task<Reservation> GetReservationById(int reservationId)
        {
            return await _context.Reservations.FirstAsync(r => r.ReservationId == reservationId);
        }

        public async Task<bool> CanUpdateReservation(Reservation reservation, ReservationFormModel model)
        {
            var reservationsInInterval = await GetReservationsForTimeInterval(model, reservation.Table);

            if (reservationsInInterval.Contains(reservation) && reservationsInInterval.Count == 1)
                return true;

            if (reservationsInInterval.Count == 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateReservation(ReservationFormModel model, Reservation reservation)
        {
            reservation.StartTime = model.StartTime;
            reservation.EndTime = model.EndTime;
            reservation.UpdatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        /*********************************************
         * 
         * 
         * AB HIER SIND DIE METHODEN VERALTET UND HABEN KEINE VERWENDUNG, SIE EXESTIEREN NUR DAMIT DIE UNIT TESTS KEINE COMPILE FHELER MACHEN
         * 
         * 
         * *******************************************/

        /*



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
            } */
    }
}
