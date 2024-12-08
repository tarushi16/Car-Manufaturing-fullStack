using Car_Manufacturing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services
{
    public interface IFinancialService
    {
        Task<Finance> GetFinanceByIdAsync(int financeId);
        Task<IEnumerable<Finance>> GetAllFinancesAsync();
        Task<Finance> CreateFinanceAsync(Finance finance);
        Task<Finance> UpdateFinanceAsync(int financeId, Finance updatedFinance);
        Task<bool> DeleteFinanceAsync(int financeId);
    }
}


