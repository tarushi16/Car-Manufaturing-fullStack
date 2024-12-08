using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    // Generic repository interface
    public interface IRepository<T> where T : class
    {
        // Get all entities of type T
        Task<List<T>> GetAllAsync();

        // Get an entity by its ID
        Task<T> GetByIdAsync(int id);

        // Add a new entity of type T
        Task<T> AddAsync(T entity);

        // Update an existing entity of type T
        Task<T> UpdateAsync(int id, T entity);

        Task<T> UpdateAsync(T entity) { throw new NotImplementedException("This method is not implemented."); }

            // Delete an entity by its ID
        Task<bool> DeleteAsync(int id);
        }
    }

