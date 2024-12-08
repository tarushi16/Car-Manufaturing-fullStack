using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class SalesOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; } // Navigation property
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
