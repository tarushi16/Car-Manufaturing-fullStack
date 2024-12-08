using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<InventoryModel> _inventoryRepository;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(IRepository<InventoryModel> inventoryRepository, ILogger<InventoryService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
        }

        public async Task<List<InventoryModel>> GetAllInventoryItemsAsync()
        {
            try
            {
                var inventoryItems = await _inventoryRepository.GetAllAsync();
                return new List<InventoryModel>(inventoryItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching inventory items.");
                throw;
            }
        }

        public async Task<InventoryModel> GetInventoryItemByIdAsync(int id)
        {
            try
            {
                return await _inventoryRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching inventory item with ID {id}.");
                throw;
            }
        }

        public async Task<InventoryModel> CreateInventoryItemAsync(InventoryModel inventory)
        {
            try
            {
                await _inventoryRepository.AddAsync(inventory);
                return inventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding inventory item.");
                throw;
            }
        }

        public async Task<InventoryModel> UpdateInventoryItemAsync(int id, InventoryModel inventory)
        {
            try
            {
                var existingInventory = await _inventoryRepository.GetByIdAsync(id);
                if (existingInventory == null)
                {
                    return null;
                }

                existingInventory.ComponentName = inventory.ComponentName;
                existingInventory.Quantity = inventory.Quantity;
                existingInventory.SupplierId = inventory.SupplierId;
                existingInventory.StockLevel = inventory.StockLevel;
                existingInventory.ReorderThreshold = inventory.ReorderThreshold;

                await _inventoryRepository.UpdateAsync(existingInventory);
                return existingInventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating inventory item with ID {id}.");
                throw;
            }
        }

        public async Task<bool> DeleteInventoryItemAsync(int id)
        {
            try
            {
                var inventoryItem = await _inventoryRepository.GetByIdAsync(id);
                if (inventoryItem == null)
                {
                    return false;
                }

                await _inventoryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting inventory item with ID {id}.");
                throw;
            }
        }
    }
}
