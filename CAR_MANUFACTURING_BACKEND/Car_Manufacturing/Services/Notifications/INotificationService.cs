using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Notifications
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<Notification> UpdateNotificationAsync(int id, Notification notification);
        Task<bool> DeleteNotificationAsync(int id);
    }
}

