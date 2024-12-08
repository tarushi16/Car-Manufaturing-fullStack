using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IQualityControlService
{
    Task<List<QualityReport>> GetAllQualityReportsAsync();
    Task<QualityReport> GetQualityReportByIdAsync(int id);
    Task<QualityReport> CreateQualityReportAsync(QualityReport qualityReport);
    Task<QualityReport> UpdateQualityReportAsync(int id, QualityReport qualityReport);
    Task<bool> DeleteQualityReportAsync(int id);
}
