using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class QualityReportRepository : IRepository<QualityReport>
    {
        private readonly ApplicationDbContext _context;

        public QualityReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all quality reports with CarModel (eager loading)
        public async Task<List<QualityReport>> GetAllAsync()
        {
            return await _context.QualityReports
                .Include(q => q.CarModel) // Include related CarModel data
                .ToListAsync();
        }

        // Get quality report by ID with CarModel (eager loading)
        public async Task<QualityReport> GetByIdAsync(int id)
        {
            return await _context.QualityReports
                .Include(q => q.CarModel) // Include related CarModel data
                .FirstOrDefaultAsync(q => q.ReportId == id); // Correct filter by ReportId
        }

        // Add a new quality report
        public async Task<QualityReport> AddAsync(QualityReport entity)
        {
            var addedReport = await _context.QualityReports.AddAsync(entity); // Add the new report
            await _context.SaveChangesAsync(); // Persist changes to the database
            return addedReport.Entity; // Return the newly added report
        }

        // Update an existing quality report by ID
        public async Task<QualityReport> UpdateAsync(int id, QualityReport entity)
        {
            var existingReport = await _context.QualityReports.FindAsync(id); // Find the report by ID
            if (existingReport == null)
                return null; // Return null if the report doesn't exist

            // Update fields of the existing report
            existingReport.CarModelId = entity.CarModelId;
            existingReport.InspectionDate = entity.InspectionDate;
            existingReport.InspectorId = entity.InspectorId;
            existingReport.TestResults = entity.TestResults;
            existingReport.DefectsFound = entity.DefectsFound;
            existingReport.Status = entity.Status;

            await _context.SaveChangesAsync(); // Persist changes to the database
            return existingReport; // Return the updated report
        }

        // Delete a quality report by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var qualityReport = await _context.QualityReports.FindAsync(id); // Find the report by ID
            if (qualityReport != null)
            {
                _context.QualityReports.Remove(qualityReport); // Remove the found report
                await _context.SaveChangesAsync(); // Persist changes to the database
                return true; // Return true if deletion was successful
            }
            return false; // Return false if the quality report doesn't exist
        }
    }
}
