using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.CarModels
{
    public class CarModelService : ICarModelService
    {
        private readonly IRepository<CarModel> _carModelRepository;

        // Constructor to inject the repository
        public CarModelService(IRepository<CarModel> carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        // Get all Car Models
        public async Task<List<CarModel>> GetAllCarModelsAsync()
        {
            return (List<CarModel>)await _carModelRepository.GetAllAsync();
        }

        // Get Car Model by ID
        public async Task<CarModel> GetCarModelByIdAsync(int id)
        {
            return await _carModelRepository.GetByIdAsync(id);
        }

        // Create a new Car Model
        public async Task<CarModel> CreateCarModelAsync(CarModel carModel)
        {
            await _carModelRepository.AddAsync(carModel);
            return carModel;
        }

        // Update an existing Car Model
        public async Task<CarModel> UpdateCarModelAsync(int id, CarModel carModel)
        {
            var existingCarModel = await _carModelRepository.GetByIdAsync(id);
            if (existingCarModel == null)
            {
                return null; // Not found
            }

            // Update fields
            existingCarModel.ModelName = carModel.ModelName;
            existingCarModel.EngineType = carModel.EngineType;
            existingCarModel.FuelEfficiency = carModel.FuelEfficiency;
            existingCarModel.DesignFeatures = carModel.DesignFeatures;
            existingCarModel.ProductionCost = carModel.ProductionCost;
            existingCarModel.Status = carModel.Status;

            await _carModelRepository.UpdateAsync(existingCarModel);
            return existingCarModel;
        }

        // Delete Car Model by ID
        public async Task<bool> DeleteCarModelAsync(int id)
        {
            var carModel = await _carModelRepository.GetByIdAsync(id);
            if (carModel == null)
            {
                return false; // Not found
            }

            await _carModelRepository.DeleteAsync(id);
            return true;
        }
    }
}
