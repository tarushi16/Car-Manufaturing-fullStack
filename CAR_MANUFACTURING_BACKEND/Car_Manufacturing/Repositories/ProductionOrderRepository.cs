using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class ProductionOrderRepository : IRepository<ProductionOrder>
    {
        private readonly ApplicationDbContext _context;

        public ProductionOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all production orders
        public async Task<List<ProductionOrder>> GetAllAsync()
        {
            return await _context.ProductionOrders.ToListAsync();
        }

        // Retrieve a production order by its ID
        public async Task<ProductionOrder> GetByIdAsync(int id)
        {
            return await _context.ProductionOrders.FindAsync(id);
        }

        // Add a new production order
        public async Task<ProductionOrder> AddAsync(ProductionOrder entity)
        {
            var addedOrder = await _context.ProductionOrders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return addedOrder.Entity; // Return the newly added production order
        }

        // Update an existing production order (using two parameters as defined in the interface)
        public async Task<ProductionOrder> UpdateAsync(int id, ProductionOrder entity)
        {
            var existingOrder = await _context.ProductionOrders.FindAsync(id);
            if (existingOrder == null)
            {
                return null; // Return null if the production order doesn't exist
            }

            // Update fields
            existingOrder.CarModelId = entity.CarModelId;
            existingOrder.StartDate = entity.StartDate;
            existingOrder.EndDate = entity.EndDate;
            existingOrder.Quantity = entity.Quantity;
            existingOrder.Status = entity.Status;

            // Save changes
            await _context.SaveChangesAsync();
            return existingOrder; // Return the updated production order
        }

        // Delete a production order
        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.ProductionOrders.FindAsync(id);
            if (order != null)
            {
                _context.ProductionOrders.Remove(order);
                await _context.SaveChangesAsync();
                return true; // Return true if deletion was successful
            }
            return false; // Return false if the production order doesn't exist
        }
    }
}
