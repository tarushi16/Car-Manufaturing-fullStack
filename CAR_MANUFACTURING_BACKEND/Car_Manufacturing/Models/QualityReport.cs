using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class QualityReport
    {
        [Key]
        public int ReportId { get; set; }
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; } // Navigation property
        public DateTime InspectionDate { get; set; }
        public int InspectorId { get; set; }
        public string TestResults { get; set; }
        public string DefectsFound { get; set; }
        public string Status { get; set; }
    }
}
