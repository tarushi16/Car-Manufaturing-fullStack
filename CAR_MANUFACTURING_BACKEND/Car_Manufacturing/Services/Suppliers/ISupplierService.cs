using Car_Manufacturing.Models;

namespace Car_Manufacturing.Services.Supplier
{
    public interface ISupplierService
    {
        Task<List<SupplierModel>> GetAllSuppliersAsync();
        Task<SupplierModel> GetSupplierByIdAsync(int id);
        Task<SupplierModel> CreateSupplierAsync(SupplierModel supplier);
        Task<SupplierModel> UpdateSupplierAsync(int id, SupplierModel supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }
}

