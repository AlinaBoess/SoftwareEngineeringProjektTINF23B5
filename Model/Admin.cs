using System.Reflection.Metadata.Ecma335;

namespace RestaurantReservierung
{
    public class Admin : UserBase
    {
        //Ctor
        public Admin(string firstName, string lastName, string email, string password) : base(firstName, lastName, email, password)
        {

        }


        public bool CreateRestaurant(ref ReservationSystem reservationSystem, Restaurant r)
        {
            if (reservationSystem == null || reservationSystem.Restaurants == null || r == null || reservationSystem.Restaurants.Contains(r))
                return false;

            reservationSystem.Restaurants.Add(r);
            return true;
        }

        public bool DeleteRestaurant(ref ReservationSystem reservationSystem, Restaurant r)
        {
            if (reservationSystem == null || reservationSystem.Restaurants == null || r == null)
                return false;

            return reservationSystem.Restaurants.Remove(r);
        }

    }
}
