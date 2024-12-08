using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class SupplierModel
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }
        public string MaterialType { get; set; }
        public int DeliveryTime { get; set; } // in days
        public decimal Pricing { get; set; }
        public string Status { get; set; }
    }
}

