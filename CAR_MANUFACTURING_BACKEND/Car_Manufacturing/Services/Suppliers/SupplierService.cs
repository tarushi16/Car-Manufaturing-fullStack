using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(ApplicationDbContext context, ILogger<SupplierService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<SupplierModel>> GetAllSuppliersAsync()
        {
            try
            {
                return await _context.Suppliers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving suppliers.");
                throw new Exception("An error occurred while retrieving suppliers.");
            }
        }

        public async Task<SupplierModel> GetSupplierByIdAsync(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    _logger.LogWarning($"Supplier with ID {id} not found.");
                    return null; // You may also throw a custom exception or return a NotFound response here
                }
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the supplier.");
                throw new Exception($"An error occurred while retrieving the supplier with ID {id}.");
            }
        }

        public async Task<SupplierModel> CreateSupplierAsync(SupplierModel supplier)
        {
            try
            {
                if (supplier == null)
                {
                    _logger.LogWarning("Received null supplier object.");
                    throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null.");
                }

                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the supplier.");
                throw new Exception("An error occurred while creating the supplier.");
            }
        }

        public async Task<SupplierModel> UpdateSupplierAsync(int id, SupplierModel supplier)
        {
            try
            {
                if (supplier == null)
                {
                    _logger.LogWarning("Received null supplier object for update.");
                    throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null.");
                }

                var existingSupplier = await _context.Suppliers.FindAsync(id);
                if (existingSupplier == null)
                {
                    _logger.LogWarning($"Supplier with ID {id} not found.");
                    return null; // Consider throwing a custom exception or returning a specific error here
                }

                // Update supplier details
                existingSupplier.Name = supplier.Name;
                existingSupplier.ContactDetails = supplier.ContactDetails;
                existingSupplier.MaterialType = supplier.MaterialType;
                existingSupplier.DeliveryTime = supplier.DeliveryTime;
                existingSupplier.Pricing = supplier.Pricing;
                existingSupplier.Status = supplier.Status;

                await _context.SaveChangesAsync();
                return existingSupplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the supplier.");
                throw new Exception($"An error occurred while updating the supplier with ID {id}.");
            }
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    _logger.LogWarning($"Supplier with ID {id} not found.");
                    return false;
                }

                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the supplier.");
                throw new Exception($"An error occurred while deleting the supplier with ID {id}.");
            }
        }
    }
}
