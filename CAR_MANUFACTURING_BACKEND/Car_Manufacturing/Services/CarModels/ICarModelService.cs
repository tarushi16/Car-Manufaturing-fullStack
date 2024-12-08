using Car_Manufacturing.Models;

namespace Car_Manufacturing.Services.CarModels
{
    public interface ICarModelService
    {
        Task<List<CarModel>> GetAllCarModelsAsync();
        Task<CarModel> GetCarModelByIdAsync(int id);
        Task<CarModel> CreateCarModelAsync(CarModel carModel);
        Task<CarModel> UpdateCarModelAsync(int id, CarModel carModel);
        Task<bool> DeleteCarModelAsync(int id);
    }
}

