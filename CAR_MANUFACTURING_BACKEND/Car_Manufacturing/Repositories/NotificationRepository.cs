using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class NotificationRepository : IRepository<Notification>
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all notifications
        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        // Retrieve a notification by its ID
        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        // Add a new notification
        public async Task<Notification> AddAsync(Notification entity)
        {
            // Assign a timestamp if not provided
            if (entity.Timestamp == default)
            {
                entity.Timestamp = DateTime.UtcNow;
            }

            await _context.Notifications.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // Return the added notification
        }

        // Update an existing notification
        public async Task<Notification> UpdateAsync(int id, Notification entity)
        {
            var existingNotification = await _context.Notifications.FindAsync(id);
            if (existingNotification == null)
            {
                return null; // Notification not found
            }

            // Update fields
            existingNotification.Type = entity.Type;
            existingNotification.Message = entity.Message;
            existingNotification.Timestamp = entity.Timestamp == default ? DateTime.UtcNow : entity.Timestamp;
            existingNotification.UserId = entity.UserId;

            await _context.SaveChangesAsync();
            return existingNotification; // Return the updated notification
        }

        // Delete a notification by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
                return true; // Deletion successful
            }

            return false; // Notification not found
        }
    }
}
