using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Production
{
    public interface IProductionService
    {
        Task<List<ProductionOrder>> GetAllProductionOrdersAsync();
        Task<ProductionOrder> GetProductionOrderByIdAsync(int id);
        Task<ProductionOrder> CreateProductionOrderAsync(ProductionOrder productionOrder);
        Task<ProductionOrder> UpdateProductionOrderAsync(int id, ProductionOrder productionOrder);
        Task<bool> DeleteProductionOrderAsync(int id);
    }
}
