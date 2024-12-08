using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class Customer
    {
        [Key]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact details are required.")]
        [MaxLength(200, ErrorMessage = "Contact details cannot exceed 200 characters.")]
        public string ContactDetails { get; set; }

        [MaxLength(1000, ErrorMessage = "Purchase history cannot exceed 1000 characters.")]
        public string PurchaseHistory { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }
    }
}
