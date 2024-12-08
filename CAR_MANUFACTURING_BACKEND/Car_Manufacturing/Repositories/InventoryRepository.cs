using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class InventoryRepository : IRepository<InventoryModel>
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all inventory items
        public async Task<List<InventoryModel>> GetAllAsync()
        {
            return await _context.InventoryModels.ToListAsync();
        }

        // Get inventory item by ID
        public async Task<InventoryModel> GetByIdAsync(int id)
        {
            return await _context.InventoryModels.FindAsync(id);
        }

        // Add a new inventory item
        public async Task<InventoryModel> AddAsync(InventoryModel entity)
        {
            await _context.InventoryModels.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // Return the added inventory item
        }

        // Update an existing inventory item
        public async Task<InventoryModel> UpdateAsync(int id, InventoryModel entity)
        {
            var inventoryItem = await _context.InventoryModels.FindAsync(id);
            if (inventoryItem == null)
            {
                return null; // Item not found
            }

            // Update properties
            inventoryItem.ComponentName = entity.ComponentName;
            inventoryItem.Quantity = entity.Quantity;
            inventoryItem.SupplierId = entity.SupplierId;
            inventoryItem.StockLevel = entity.StockLevel;
            inventoryItem.ReorderThreshold = entity.ReorderThreshold;

            _context.InventoryModels.Update(inventoryItem);
            await _context.SaveChangesAsync();

            return inventoryItem; // Return the updated inventory item
        }

        // Delete an inventory item by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var inventoryItem = await _context.InventoryModels.FindAsync(id);
            if (inventoryItem != null)
            {
                _context.InventoryModels.Remove(inventoryItem);
                await _context.SaveChangesAsync();
                return true; // Deletion successful
            }

            return false; // Item not found
        }
    }
}
