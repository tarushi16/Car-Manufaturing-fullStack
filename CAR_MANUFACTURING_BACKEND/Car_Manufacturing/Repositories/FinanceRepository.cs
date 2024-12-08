using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class FinanceRepository : IRepository<Finance>
    {
        private readonly ApplicationDbContext _context;

        public FinanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all finance records
        public async Task<List<Finance>> GetAllAsync()
        {
            return await _context.Finances.ToListAsync();
        }

        // Get a finance record by ID
        public async Task<Finance> GetByIdAsync(int id)
        {
            return await _context.Finances.FindAsync(id);
        }

        // Add a new finance record
        public async Task<Finance> AddAsync(Finance entity)
        {
            await _context.Finances.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // Return the added finance record
        }

        // Update an existing finance record
        public async Task<Finance> UpdateAsync(int id, Finance entity)
        {
            var finance = await _context.Finances.FindAsync(id);
            if (finance == null)
            {
                return null; // Record not found
            }

            // Update properties
            finance.TransactionType = entity.TransactionType;
            finance.Amount = entity.Amount;
            finance.Date = entity.Date;
            finance.Details = entity.Details;

            _context.Finances.Update(finance);
            await _context.SaveChangesAsync();

            return finance; // Return the updated finance record
        }

        // Delete a finance record by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var finance = await _context.Finances.FindAsync(id);
            if (finance != null)
            {
                _context.Finances.Remove(finance);
                await _context.SaveChangesAsync();
                return true; // Deletion successful
            }

            return false; // Finance record not found
        }
    }
}
