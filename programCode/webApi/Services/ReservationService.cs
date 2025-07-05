using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class ReservationService
    {
        //already initialized this way, therefore no extra constructor is needed
        List<Restaurant> restaurants = new List<Restaurant>();
        List<User> users = new List<User>();

        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly TableService _tableService;

        // The min Timespan how long a reservation needs to be.
        public TimeSpan MinReservationTime { get; } = new TimeSpan(1, 0, 0);

        public ReservationService(AppDbContext context, UserService userService, TableService tableService)
        {
            _context = context;
            _userService = userService;
            _tableService = tableService;
        }

        public async Task<bool> ReserveAsync(ReservationFormModel model, int tableId)
        {        
            var user = await _userService.GetLoggedInUserAsync() ?? throw new Exception("User nicht gefunden");
            var table = await _tableService.GetTableByIdAsync(tableId) ?? throw new BadHttpRequestException("The table does not exist." );

            if (model.EndTime <= model.StartTime)
                throw new BadHttpRequestException("Illegal time interval!" );

            if (!IsGreaterThenMinTimeInterval(model))
                throw new BadHttpRequestException("The reservation time interval has to be >= " + MinReservationTime.ToString() + "." );

            if (IsInPast(model))
                throw new BadHttpRequestException("The given Time interval is in the past!" );

            if ((await GetReservationsForTimeIntervalAsync(model, table)).Count > 0)
                throw new BadHttpRequestException("There already exists a reservation in the given time interval!" );


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

        public async Task<List<Reservation>> GetReservationsForTimeIntervalAsync(ReservationFormModel model, Table table)
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

        public async Task<List<Reservation>> GetReservationsAsync(ReservationFilterModel model)
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

        }

        public async Task<List<Reservation>> GetReservationsForRestaurantsAsync(List<Restaurant> restaurants, ReservationFilterModel model)
        {
            var reservations = new List<Reservation>();

            foreach (var restaurant in restaurants)
            {
                model.RestaurantId = restaurant.RestaurantId;
                reservations = [.. reservations, .. await GetReservationsAsync(model)];
            }
            return reservations;
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await _context.Reservations.FirstAsync(r => r.ReservationId == reservationId);
        }

        public async Task<bool> CanUpdateReservationAsync(Reservation reservation, ReservationFormModel model)
        {
            var reservationsInInterval = await GetReservationsForTimeIntervalAsync(model, reservation.Table);

            if (reservationsInInterval.Contains(reservation) && reservationsInInterval.Count == 1)
                return true;

            if (reservationsInInterval.Count == 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateReservationAsync(ReservationFormModel model, int reservationId)
        {
            var user = await _userService.GetLoggedInUserAsync();
            var reservation = await GetReservationByIdAsync(reservationId);

            if (user.UserId != reservation.UserId && user.Role != "ADMIN")
                throw new BadHttpRequestException("You don't have Permissions to change the reservation");

            if (model.EndTime <= model.StartTime)
                throw new BadHttpRequestException("Illegal time interval!");

            if (!IsGreaterThenMinTimeInterval(model))
                throw new BadHttpRequestException("The reservation time interval has to be >= " + MinReservationTime.ToString() + "." );

            if (IsInPast(model))
                throw new BadHttpRequestException("The given Time interval is in the past!");

            if (!await CanUpdateReservationAsync(reservation, model))
                throw new BadHttpRequestException("There already exists a reservation in the given time interval!");


            reservation.StartTime = model.StartTime;
            reservation.EndTime = model.EndTime;
            reservation.UpdatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteReservationAsync(int reservationId)
        {
            var user = await _userService.GetLoggedInUserAsync();

            var reservation = await GetReservationByIdAsync(reservationId);

            if (user != reservation.User && user.Role != "ADMIN" && user != reservation.Table.Restaurant.User)
                throw new BadHttpRequestException("Your dont have permissions to perform this action!");


            _context.Reservations.Remove(reservation);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
