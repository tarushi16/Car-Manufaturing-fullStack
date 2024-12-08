using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class CarModel
    {
        [Key]
        public int? ModelId { get; set; } 
        public string ModelName { get; set; }
        public string EngineType { get; set; }
        public string FuelEfficiency { get; set; }
        public string DesignFeatures { get; set; }
        public decimal ProductionCost { get; set; }
        public string Status { get; set; } // Example: "Active", "Discontinued"
    }

}
