using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> CreateFeedback(User user, FeedbackFormModel model, Reservation reservation)
        {
            var feedback = new Feedback
            {
                Rating = model.Rating,
                Comment = model.Comment,
                User = user,
                Reservation = reservation,
                CreatedAt = DateTime.Now,
            };

            _context.Feedbacks.Add(feedback);

            if(await _context.SaveChangesAsync() > 0) 
                return true;

            return false;

        }

        public async Task<bool> DeleteFeedback(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Feedback> GetFeedbackById(int feedbackId)
        {
            return await _context.Feedbacks.FirstAsync(f => f.FeedbackId == feedbackId);
        }
        /*
        public async Task<float> CalcRestaurantRating(Restaurant restaurant)
        {

        }
        */
    }
}
