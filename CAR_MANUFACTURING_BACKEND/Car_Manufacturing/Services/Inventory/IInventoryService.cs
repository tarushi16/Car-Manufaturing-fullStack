using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Inventory
{
    public interface IInventoryService
    {
        Task<List<InventoryModel>> GetAllInventoryItemsAsync();
        Task<InventoryModel> GetInventoryItemByIdAsync(int id);
        Task<InventoryModel> CreateInventoryItemAsync(InventoryModel inventory);
        Task<InventoryModel> UpdateInventoryItemAsync(int id, InventoryModel inventory);
        Task<bool> DeleteInventoryItemAsync(int id);
    }
}

