using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Car_Manufacturing.Services.Sales
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalesOrderService> _logger;

        public SalesOrderService(ApplicationDbContext context, ILogger<SalesOrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<SalesOrder>> GetAllSalesOrdersAsync()
        {
            try
            {
                return await _context.SalesOrders
                    .Include(so => so.CarModel) // Eager loading for navigation property
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching all sales orders: {ex.Message}");
                throw new ApplicationException("An error occurred while fetching sales orders.");
            }
        }

        public async Task<SalesOrder> GetSalesOrderByIdAsync(int id)
        {
            try
            {
                var salesOrder = await _context.SalesOrders
                    .Include(so => so.CarModel) // Include CarModel details
                    .FirstOrDefaultAsync(so => so.OrderId == id);

                if (salesOrder == null)
                {
                    _logger.LogWarning($"Sales order with ID {id} not found.");
                    return null;
                }

                return salesOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching sales order with ID {id}: {ex.Message}");
                throw new ApplicationException($"An error occurred while fetching sales order with ID {id}.");
            }
        }

        public async Task<SalesOrder> CreateSalesOrderAsync(SalesOrder salesOrder)
        {
            try
            {
                if (salesOrder == null)
                {
                    throw new ArgumentNullException(nameof(salesOrder), "Sales order cannot be null.");
                }

                _context.SalesOrders.Add(salesOrder);
                await _context.SaveChangesAsync();
                return salesOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating sales order: {ex.Message}");
                throw new ApplicationException("An error occurred while creating the sales order.");
            }
        }

        public async Task<SalesOrder> UpdateSalesOrderAsync(int id, SalesOrder salesOrder)
        {
            try
            {
                var existingOrder = await _context.SalesOrders.FindAsync(id);
                if (existingOrder == null)
                {
                    _logger.LogWarning($"Sales order with ID {id} not found for update.");
                    return null;
                }

                // Update properties
                existingOrder.CustomerId = salesOrder.CustomerId;
                existingOrder.CarModelId = salesOrder.CarModelId;
                existingOrder.OrderDate = salesOrder.OrderDate;
                existingOrder.DeliveryDate = salesOrder.DeliveryDate;
                existingOrder.Price = salesOrder.Price;
                existingOrder.Status = salesOrder.Status;

                await _context.SaveChangesAsync();
                return existingOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating sales order with ID {id}: {ex.Message}");
                throw new ApplicationException($"An error occurred while updating sales order with ID {id}.");
            }
        }

        public async Task<bool> DeleteSalesOrderAsync(int id)
        {
            try
            {
                var salesOrder = await _context.SalesOrders.FindAsync(id);
                if (salesOrder == null)
                {
                    _logger.LogWarning($"Sales order with ID {id} not found for deletion.");
                    return false;
                }

                _context.SalesOrders.Remove(salesOrder);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting sales order with ID {id}: {ex.Message}");
                throw new ApplicationException($"An error occurred while deleting sales order with ID {id}.");
            }
        }
    }
}
