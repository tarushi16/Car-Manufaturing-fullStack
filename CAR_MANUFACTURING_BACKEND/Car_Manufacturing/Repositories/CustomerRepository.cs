using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all customers
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // Get a customer by ID
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        // Add a new customer
        public async Task<Customer> AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // Return the added customer
        }

        // Update an existing customer
        public async Task<Customer> UpdateAsync(int id, Customer entity)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null; // Or handle appropriately (e.g., throw an exception)
            }

            // Update customer properties
            customer.Name = entity.Name;
            customer.ContactDetails = entity.ContactDetails;
            customer.PurchaseHistory = entity.PurchaseHistory;
            customer.Status = entity.Status;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer; // Return the updated customer
        }

        // Delete a customer by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true; // Deletion successful
            }

            return false; // Customer not found
        }
    }
}
