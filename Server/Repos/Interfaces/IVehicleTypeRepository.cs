using BlazorApp1.Server.Entities;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IVehicleTypeRepository
    {
        Task AddAsync(VehicleTypeEntity entity);
        Task<VehicleTypeEntity> UpdateAsync(VehicleTypeEntity entity);
        Task<bool> RestoreVehicleTypeAsync(int vehicleTypeID);
        Task<bool> DeleteVehicleTypeAsync(int vehicleTypeID);
        Task<List<VehicleTypeEntity>> GetVehicleTypeByCompanyAsync(string companyName);
        Task<List<VehicleTypeEntity>> GetVehicleByIDAsync(int VehicleTypeID);
        Task<List<VehicleTypeEntity>> GetVehicleTypeByCompanyTestAsync();
    }
}
