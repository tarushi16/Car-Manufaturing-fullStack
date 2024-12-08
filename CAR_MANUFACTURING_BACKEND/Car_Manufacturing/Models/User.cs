using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
        

    }
}
