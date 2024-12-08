using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Reporting
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _reportRepository;

        public ReportService(IRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // Get all reports
        public async Task<List<Report>> GetAllReportsAsync()
        {
            try
            {
                return await _reportRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                throw new Exception("An error occurred while retrieving reports.", ex);
            }
        }

        // Get report by ID
        public async Task<Report> GetReportByIdAsync(int id)
        {
            try
            {
                var report = await _reportRepository.GetByIdAsync(id);
                if (report == null)
                {
                    // Handle not found case
                    throw new KeyNotFoundException($"Report with ID {id} not found.");
                }
                return report;
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                throw new Exception($"An error occurred while retrieving the report with ID {id}.", ex);
            }
        }

        // Create a new report
        public async Task<Report> CreateReportAsync(Report report)
        {
            try
            {
                if (report == null)
                {
                    throw new ArgumentNullException(nameof(report), "Report cannot be null.");
                }
                return await _reportRepository.AddAsync(report);
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                throw new Exception("An error occurred while creating the report.", ex);
            }
        }

        // Update an existing report
        public async Task<Report> UpdateReportAsync(int id, Report report)
        {
            try
            {
                if (report == null)
                {
                    throw new ArgumentNullException(nameof(report), "Report cannot be null.");
                }

                var existingReport = await _reportRepository.GetByIdAsync(id);
                if (existingReport == null)
                {
                    throw new KeyNotFoundException($"Report with ID {id} not found.");
                }

                existingReport.Type = report.Type;
                existingReport.GeneratedDate = report.GeneratedDate;
                existingReport.Data = report.Data;
                existingReport.CreatedBy = report.CreatedBy;

                return await _reportRepository.UpdateAsync(id, existingReport);
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                throw new Exception($"An error occurred while updating the report with ID {id}.", ex);
            }
        }

        // Delete a report
        public async Task<bool> DeleteReportAsync(int id)
        {
            try
            {
                var result = await _reportRepository.DeleteAsync(id);
                if (!result)
                {
                    throw new KeyNotFoundException($"Report with ID {id} not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                throw new Exception($"An error occurred while deleting the report with ID {id}.", ex);
            }
        }
    }
}
