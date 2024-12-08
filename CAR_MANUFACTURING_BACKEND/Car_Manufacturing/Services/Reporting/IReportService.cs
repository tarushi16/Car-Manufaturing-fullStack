using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Reporting
{
    public interface IReportService
    {
        Task<List<Report>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(int id);
        Task<Report> CreateReportAsync(Report report);
        Task<Report> UpdateReportAsync(int id, Report report);
        Task<bool> DeleteReportAsync(int id);
    }
}
