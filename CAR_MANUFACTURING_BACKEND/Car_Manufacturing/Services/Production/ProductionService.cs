using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Production
{
    public class ProductionService : IProductionService
    {
        private readonly IRepository<ProductionOrder> _productionOrderRepository;
        private readonly ILogger<ProductionService> _logger;

        public ProductionService(IRepository<ProductionOrder> productionOrderRepository, ILogger<ProductionService> logger)
        {
            _productionOrderRepository = productionOrderRepository;
            _logger = logger;
        }

        // Retrieve all production orders
        public async Task<List<ProductionOrder>> GetAllProductionOrdersAsync()
        {
            try
            {
                return await _productionOrderRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all production orders.");
                throw;
            }
        }

        // Retrieve a production order by its ID
        public async Task<ProductionOrder> GetProductionOrderByIdAsync(int id)
        {
            try
            {
                return await _productionOrderRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving production order with ID {id}.");
                throw;
            }
        }

        // Create a new production order
        public async Task<ProductionOrder> CreateProductionOrderAsync(ProductionOrder productionOrder)
        {
            try
            {
                return await _productionOrderRepository.AddAsync(productionOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a production order.");
                throw;
            }
        }

        // Update an existing production order
        public async Task<ProductionOrder> UpdateProductionOrderAsync(int id, ProductionOrder productionOrder)
        {
            try
            {
                productionOrder.OrderId = id; // Ensure the production order ID is set
                return await _productionOrderRepository.UpdateAsync(productionOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating production order with ID {id}.");
                throw;
            }
        }

        // Delete a production order
        public async Task<bool> DeleteProductionOrderAsync(int id)
        {
            try
            {
                return await _productionOrderRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting production order with ID {id}.");
                throw;
            }
        }
    }
}
