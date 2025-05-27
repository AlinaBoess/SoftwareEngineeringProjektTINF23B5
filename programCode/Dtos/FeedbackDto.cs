using RestaurantReservierung.Models;
using System.Text.Json.Serialization;

namespace RestaurantReservierung.Dtos
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }

        public int UserId { get; set; }

        public int ReservationId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int RestaurantId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ReservationDto Reservation { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RestaurantDto Restaurant { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UserDto User { get; set; } = null!;

        public static FeedbackDto MapToDto(Feedback feedback)
        {
            return new FeedbackDto
            {
                FeedbackId = feedback.FeedbackId,
                UserId = feedback.UserId,
                ReservationId = feedback.ReservationId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt,

                User = new UserDto
                {
                    UserId = feedback.UserId,
                    FirstName = feedback.User.FirstName,
                    LastName = feedback.User.LastName,
                }
            };
        }

        public static List<FeedbackDto> MapToDtos(List<Feedback> feedbacks)
        {
            var feedbackDtos = new List<FeedbackDto>();

            foreach (var feedback in feedbacks)
            {
                feedbackDtos.Add(MapToDto(feedback));
            }

            return feedbackDtos;
        }
    }

    
}
