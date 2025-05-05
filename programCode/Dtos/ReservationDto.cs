using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Dtos
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }

        public int TableId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual TableDto Table { get; set; }

        public virtual UserDto User { get; set; }

        
        public static ReservationDto MapToDto(Reservation reservation)
        {
            return new ReservationDto
            {
                ReservationId = reservation.ReservationId,
                TableId = reservation.TableId,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt,
                
                Table = new TableDto
                {
                    TableId = reservation.TableId,                   
                    TableNr = reservation.Table.TableNr,
                    Capacity = reservation.Table.Capacity,
                    Area = reservation.Table.Area,
                },
                User = new UserDto
                {
                    UserId = reservation.User.UserId,
                    FirstName = reservation.User.FirstName,
                    LastName = reservation.User.LastName,
                }

            };
        }

        public static List<ReservationDto> MapToDtos(List<Reservation> reservations)
        {
            var reservationDtos = new List<ReservationDto>();

            foreach (var reservation in reservations)
            {
                reservationDtos.Add(MapToDto(reservation));
            }

            return reservationDtos;
        }
    }
}
