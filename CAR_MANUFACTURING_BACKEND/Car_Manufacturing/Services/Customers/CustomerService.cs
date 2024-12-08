using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        // Constructor to inject the repository and logger
        public CustomerService(IRepository<Customer> customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        // Get all Customers
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return (List<Customer>)await _customerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all customers.");
                throw;
            }
        }

        // Get Customer by ID
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _customerRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer with ID {id}.");
                throw;
            }
        }

        // Create a new Customer
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            try
            {
                await _customerRepository.AddAsync(customer);
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new customer.");
                throw;
            }
        }

        // Update an existing Customer
        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return null; // Not found
                }

                // Update fields
                existingCustomer.Name = customer.Name;
                existingCustomer.ContactDetails = customer.ContactDetails;
                existingCustomer.PurchaseHistory = customer.PurchaseHistory;
                existingCustomer.Status = customer.Status;

                await _customerRepository.UpdateAsync(existingCustomer);
                return existingCustomer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating customer with ID {id}.");
                throw;
            }
        }

        // Delete Customer by ID
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return false; // Not found
                }

                await _customerRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting customer with ID {id}.");
                throw;
            }
        }
    }
}
