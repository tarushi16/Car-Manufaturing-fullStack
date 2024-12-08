using System.ComponentModel.DataAnnotations;

namespace Car_Manufacturing.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public string Type { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string Data { get; set; }
        public int CreatedBy { get; set; } // UserId of the creator
    }
}
