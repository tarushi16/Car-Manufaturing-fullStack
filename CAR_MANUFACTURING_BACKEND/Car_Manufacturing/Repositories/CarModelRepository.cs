using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories
{
    public class CarModelRepository : IRepository<CarModel>
    {
        private readonly ApplicationDbContext _context;

        public CarModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarModel>> GetAllAsync()
        {
            return await _context.CarModels.ToListAsync(); // Ensure List<CarModel> is returned
        }

        public async Task<CarModel> GetByIdAsync(int id)
        {
            return await _context.CarModels.FindAsync(id);
        }

        public async Task<CarModel> AddAsync(CarModel entity)
        {
            await _context.CarModels.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // Return the added entity
        }

        public async Task<CarModel> UpdateAsync(int id, CarModel entity)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return null; // Or handle it as needed
            }

            // Update the properties of the existing car model
            carModel.ModelName = entity.ModelName;
            carModel.EngineType = entity.EngineType;
            carModel.FuelEfficiency = entity.FuelEfficiency;
            carModel.DesignFeatures = entity.DesignFeatures;
            carModel.ProductionCost = entity.ProductionCost;
            carModel.Status = entity.Status;

            _context.CarModels.Update(carModel);
            await _context.SaveChangesAsync();

            return carModel; // Return the updated entity
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModels.Remove(carModel);
                await _context.SaveChangesAsync();
                return true; // Return true if deletion was successful
            }

            return false; // Return false if the entity was not found
        }
    }
}
