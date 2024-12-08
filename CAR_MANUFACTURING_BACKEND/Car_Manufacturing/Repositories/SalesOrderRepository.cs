using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories.Sales
{
    public class SalesOrderRepository : IRepository<SalesOrder>
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all sales orders
        public async Task<List<SalesOrder>> GetAllAsync()
        {
            return await _context.SalesOrders
                .Include(so => so.CarModel) // Eager loading CarModel for related data
                .ToListAsync();
        }

        // Get a sales order by its ID
        public async Task<SalesOrder> GetByIdAsync(int id)
        {
            return await _context.SalesOrders
                .Include(so => so.CarModel) // Include CarModel details
                .FirstOrDefaultAsync(so => so.OrderId == id); // Use correct filter
        }

        // Add a new sales order
        public async Task<SalesOrder> AddAsync(SalesOrder salesOrder)
        {
            var addedOrder = await _context.SalesOrders.AddAsync(salesOrder); // Add the new sales order
            await _context.SaveChangesAsync(); // Save changes to the database
            return addedOrder.Entity; // Return the newly added order
        }

        // Update an existing sales order by ID
        public async Task<SalesOrder> UpdateAsync(int id, SalesOrder salesOrder)
        {
            var existingSalesOrder = await _context.SalesOrders.FindAsync(id); // Find the sales order by ID
            if (existingSalesOrder == null)
                return null; // Exit if the sales order doesn't exist

            // Update properties
            existingSalesOrder.CustomerId = salesOrder.CustomerId;
            existingSalesOrder.CarModelId = salesOrder.CarModelId;
            existingSalesOrder.OrderDate = salesOrder.OrderDate;
            existingSalesOrder.DeliveryDate = salesOrder.DeliveryDate;
            existingSalesOrder.Price = salesOrder.Price;
            existingSalesOrder.Status = salesOrder.Status;

            await _context.SaveChangesAsync(); // Save changes to the database
            return existingSalesOrder; // Return the updated sales order
        }

        // Delete a sales order by its ID
        public async Task<bool> DeleteAsync(int id)
        {
            var salesOrder = await _context.SalesOrders.FindAsync(id); // Find the sales order by ID
            if (salesOrder != null)
            {
                _context.SalesOrders.Remove(salesOrder); // Remove the found sales order
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Return true if deletion was successful
            }
            return false; // Return false if the sales order doesn't exist
        }
    }
}
