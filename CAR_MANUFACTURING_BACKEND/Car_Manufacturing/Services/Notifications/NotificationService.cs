using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;

        public NotificationService(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // Retrieve all notifications
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        // Retrieve a notification by its ID
        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        // Create a new notification
        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            return await _notificationRepository.AddAsync(notification);
            //await _notificationRepository.AddAsync(notification);

        }

        // Update an existing notification
        public async Task<Notification> UpdateNotificationAsync(int id, Notification notification)
        {
            notification.NotificationId = id; // Ensure the notification ID is set
            return await _notificationRepository.UpdateAsync(id, notification);
        }

        // Delete a notification
        public async Task<bool> DeleteNotificationAsync(int id)
        {
            return await _notificationRepository.DeleteAsync(id);
        }
    }
}
