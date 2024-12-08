using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Sales
{
    public interface ISalesOrderService
    {
        Task<List<SalesOrder>> GetAllSalesOrdersAsync();
        Task<SalesOrder> GetSalesOrderByIdAsync(int id);
        Task<SalesOrder> CreateSalesOrderAsync(SalesOrder salesOrder);
        Task<SalesOrder> UpdateSalesOrderAsync(int id, SalesOrder salesOrder);
        Task<bool> DeleteSalesOrderAsync(int id);
    }
}

