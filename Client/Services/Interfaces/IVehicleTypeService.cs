using BlazorApp1.Shared.VehicleTypeDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypeByCompany(string company);
        Task Create(CreateVehicleTypeDTO createVehicleTypeDTO);
        Task Update(UpdateVehicleTypeDTO updateVehicleTypeDTO);
    }
}
