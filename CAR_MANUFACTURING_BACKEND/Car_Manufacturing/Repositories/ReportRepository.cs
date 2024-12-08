using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class ReportRepository : IRepository<Report>
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all reports
        public async Task<List<Report>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        // Get a report by its ID
        public async Task<Report> GetByIdAsync(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id); // Filter by ReportId
        }

        // Add a new report
        public async Task<Report> AddAsync(Report entity)
        {
            var addedReport = await _context.Reports.AddAsync(entity); // Add the new report
            await _context.SaveChangesAsync(); // Persist changes to the database
            return addedReport.Entity; // Return the newly added report
        }

        // Update an existing report
        public async Task<Report> UpdateAsync(int id, Report entity)
        {
            var existingReport = await _context.Reports.FindAsync(id); // Find the report by ID
            if (existingReport == null)
                return null; // Return null if the report doesn't exist

            // Update the fields of the existing report
            existingReport.Type = entity.Type;
            existingReport.GeneratedDate = entity.GeneratedDate;
            existingReport.Data = entity.Data;
            existingReport.CreatedBy = entity.CreatedBy;

            await _context.SaveChangesAsync(); // Persist changes to the database
            return existingReport; // Return the updated report
        }

        // Delete a report by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id); // Find the report by ID
            if (report != null)
            {
                _context.Reports.Remove(report); // Remove the found report
                await _context.SaveChangesAsync(); // Persist changes to the database
                return true; // Return true if deletion was successful
            }
            return false; // Return false if the report doesn't exist
        }
    }
}
