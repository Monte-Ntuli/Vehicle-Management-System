using BlazorApp1.Shared.VehiclesDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDTO>> GetVehicleByCompany(string company);
        Task Create(CreateVehicleDTO createVehicleDTO);
        Task<IEnumerable<VehicleDTO>> GetVehicleByID(int VehicleID);
        Task Update(UpdateVehicleDTO updateVehicleDTO);
    }
}
