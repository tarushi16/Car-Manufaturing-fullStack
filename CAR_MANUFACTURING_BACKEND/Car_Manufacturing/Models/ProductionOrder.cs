using System.ComponentModel.DataAnnotations;

public class ProductionOrder
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public int CarModelId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }
}

