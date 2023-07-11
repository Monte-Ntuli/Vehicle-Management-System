using BlazorApp1.Server.Entities;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IVehicleMakeRepository
    {
        Task AddAsync(VehicleMakeEntity entity);
        Task<bool> DeleteVehicleMakeAsync(int VehicleID);
        Task<bool> RestoreVehicleMakeAsync(int VehicleID);
        Task<List<VehicleMakeEntity>> GetVehicleMakeByIDAsync(int VehicleID);
        Task<List<VehicleMakeEntity>> GetVehicleMakeByCompanyAsync(string Company);
        
        Task<VehicleMakeEntity> UpdateAsync(VehicleMakeEntity entity);
        Task<List<VehicleMakeEntity>> GetVehicleByCompanyTestAsync();
    }
}
