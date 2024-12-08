using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IRepository<Finance> _financeRepository;
        private readonly ILogger<FinancialService> _logger;

        // Constructor to inject the repository and logger
        public FinancialService(IRepository<Finance> financeRepository, ILogger<FinancialService> logger)
        {
            _financeRepository = financeRepository;
            _logger = logger;
        }

        // Get Finance by ID
        public async Task<Finance> GetFinanceByIdAsync(int financeId)
        {
            try
            {
                return await _financeRepository.GetByIdAsync(financeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching finance with ID {financeId}.");
                throw;
            }
        }

        // Get all Finances
        public async Task<IEnumerable<Finance>> GetAllFinancesAsync()
        {
            try
            {
                return await _financeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all finances.");
                throw;
            }
        }

        // Create a new Finance record
        public async Task<Finance> CreateFinanceAsync(Finance finance)
        {
            try
            {
                await _financeRepository.AddAsync(finance);
                return finance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a finance record.");
                throw;
            }
        }

        // Update an existing Finance record
        public async Task<Finance> UpdateFinanceAsync(int financeId, Finance updatedFinance)
        {
            try
            {
                var finance = await _financeRepository.GetByIdAsync(financeId);
                if (finance == null)
                {
                    return null; // Not found
                }

                // Update fields
                finance.TransactionType = updatedFinance.TransactionType;
                finance.Amount = updatedFinance.Amount;
                finance.Date = updatedFinance.Date;
                finance.Details = updatedFinance.Details;

                await _financeRepository.UpdateAsync(finance);
                return finance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating finance with ID {financeId}.");
                throw;
            }
        }

        // Delete Finance record by ID
        public async Task<bool> DeleteFinanceAsync(int financeId)
        {
            try
            {
                var finance = await _financeRepository.GetByIdAsync(financeId);
                if (finance == null)
                {
                    return false; // Not found
                }

                await _financeRepository.DeleteAsync(financeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting finance with ID {financeId}.");
                throw;
            }
        }
    }
}
