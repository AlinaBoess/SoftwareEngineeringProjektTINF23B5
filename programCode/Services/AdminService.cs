using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public static class AdminService
    {
        public static bool CreateRestaurant(ref ReservationSystem reservationSystem, Restaurant r)
        {
            if (reservationSystem == null || reservationSystem.Restaurants == null || r == null || reservationSystem.Restaurants.Contains(r))
                return false;

            reservationSystem.Restaurants.Add(r);
            return true;
        }

        public static bool DeleteRestaurant(ref ReservationSystem reservationSystem, Restaurant r)
        {
            if (reservationSystem == null || reservationSystem.Restaurants == null || r == null)
                return false;

            return reservationSystem.Restaurants.Remove(r);
        }
    }
}
