using BlazorApp1.Server.Entities;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IVehicleModelTypeRepository
    {
        Task AddAsync(VehicleModelTypeEntity entity);
        Task<bool> DeleteVehicleModelTypeAsync(int VehicleID);
        Task<bool> RestoreVehicleModelTypeAsync(int VehicleID);
        Task<List<VehicleModelTypeEntity>> GetVehicleModelTypeByIDAsync(int VehicleID);
        Task<List<VehicleModelTypeEntity>> GetVehicleModelTypeByCompanyAsync(string Company);
        
        Task<VehicleModelTypeEntity> UpdateAsync(VehicleModelTypeEntity entity);
        Task<List<VehicleModelTypeEntity>> GetVehicleByCompanyTestAsync();
    }
}
