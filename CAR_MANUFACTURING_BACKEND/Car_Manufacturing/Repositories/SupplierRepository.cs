using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories.Suppliers
{
    public class SupplierRepository : IRepository<SupplierModel>
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all suppliers
        public async Task<List<SupplierModel>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // Get supplier by ID
        public async Task<SupplierModel> GetByIdAsync(int id)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);
        }

        // Add a new supplier
        public async Task<SupplierModel> AddAsync(SupplierModel supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier; // Return the added supplier
        }

        // Update an existing supplier by ID and entity
        public async Task<SupplierModel> UpdateAsync(int id, SupplierModel supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            var existingSupplier = await _context.Suppliers.FindAsync(id);
            if (existingSupplier == null)
                return null;

            existingSupplier.Name = supplier.Name;
            existingSupplier.ContactDetails = supplier.ContactDetails;
            existingSupplier.MaterialType = supplier.MaterialType;
            existingSupplier.DeliveryTime = supplier.DeliveryTime;
            existingSupplier.Pricing = supplier.Pricing;
            existingSupplier.Status = supplier.Status;

            await _context.SaveChangesAsync();
            return existingSupplier;
        }

        // Delete a supplier by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
