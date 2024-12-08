using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class InventoryModel
    {
        [Key]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "Component Name is required.")]
        [MaxLength(200, ErrorMessage = "Component Name cannot exceed 200 characters.")]
        public string ComponentName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Supplier ID is required.")]
        public int SupplierId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock Level must be a positive number.")]
        public int StockLevel { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder Threshold must be a positive number.")]
        public int ReorderThreshold { get; set; }
    }
}
