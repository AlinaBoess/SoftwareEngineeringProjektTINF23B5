using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class FeedbackService
    {
        private readonly AppDbContext _context;

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateFeedbackAsync(User user, FeedbackFormModel model, Restaurant restaurant, Reservation reservation)
        {
            var feedback = new Feedback
            {
                Rating = model.Rating,
                Comment = model.Comment,
                User = user,
                Reservation = reservation,
                CreatedAt = DateTime.Now,
                Restaurant = restaurant
            };

            _context.Feedbacks.Add(feedback);

            if(await _context.SaveChangesAsync() > 0) 
                return true;

            return false;

        }

        public async Task<bool> DeleteFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _context.Feedbacks.FirstAsync(f => f.FeedbackId == feedbackId);
        }
        
        public async Task<double> CalcRestaurantRatingAsync(Restaurant restaurant)
        {
            var averageRating = await _context.Feedbacks
            .Where(f => f.Restaurant == restaurant)
            .AverageAsync(f => (double)f.Rating);

            return averageRating;
        }

        public async Task<List<Feedback>> GetFeedbacksForRestaurantAsync(Restaurant restaurant)
        {
            return await _context.Feedbacks
                .Where(f => f.Restaurant == restaurant)
                .ToListAsync();
        }
    }
}
