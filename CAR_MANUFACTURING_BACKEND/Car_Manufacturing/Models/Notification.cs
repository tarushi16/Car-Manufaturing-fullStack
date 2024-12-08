using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public int UserId { get; set; } // UserId of the receiver
    }
}
