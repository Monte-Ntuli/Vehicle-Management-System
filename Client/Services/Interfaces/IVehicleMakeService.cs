using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMakeDTO>> GetVehicleModelTypeByCompany(string company);
        Task Create(CreateVehicleMakeDTO createVehicleTypeDTO);
    }
}
