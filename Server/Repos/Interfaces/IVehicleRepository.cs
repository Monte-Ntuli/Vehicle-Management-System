using BlazorApp1.Server.Entities;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IVehicleRepository 
    {
        Task<VehicleEntity> AddAsync(VehicleEntity entity);
        Task<bool> RestoreVehicleAsync(int VehicleID);
        Task<bool> DeleteVehicleAsync(int VehicleID);
        Task<List<VehicleEntity>> GetVehicleByIDAsync(int VehicleID);
        Task<List<VehicleEntity>> GetVehicleByRegAsync(string Registration);
        Task<List<VehicleEntity>> GetVehicleByCompanyAsync(string Company);
        Task<List<VehicleEntity>> GetVehicleByTypeAsync(int vehicleType);
        Task<VehicleEntity> UpdateAsync(VehicleEntity entity);
    }
}
