// QualityControlService.cs
using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.QualityControl
{
    public class QualityControlService : IQualityControlService
    {
        private readonly IRepository<QualityReport> _qualityReportRepository;

        public QualityControlService(IRepository<QualityReport> qualityReportRepository)
        {
            _qualityReportRepository = qualityReportRepository;
        }

        public async Task<List<QualityReport>> GetAllQualityReportsAsync()
        {
            return await _qualityReportRepository.GetAllAsync();
        }

        public async Task<QualityReport> GetQualityReportByIdAsync(int id)
        {
            return await _qualityReportRepository.GetByIdAsync(id);
        }

        public async Task<QualityReport> CreateQualityReportAsync(QualityReport qualityReport)
        {
            return await _qualityReportRepository.AddAsync(qualityReport);
        }

        public async Task<QualityReport> UpdateQualityReportAsync(int id, QualityReport qualityReport)
        {
            return await _qualityReportRepository.UpdateAsync(id, qualityReport); // Correct return type
        }

        public async Task<bool> DeleteQualityReportAsync(int id)
        {
            return await _qualityReportRepository.DeleteAsync(id);
        }
    }
}
