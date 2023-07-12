using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly HttpClient _httpClient;
        public IEnumerable<VehicleMakeDTO> VehicleModelTypes { get; set; } = new List<VehicleMakeDTO>();
        public VehicleMakeDTO vehicleModelType { get; set; } = new VehicleMakeDTO();
        public VehicleMakeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<VehicleMakeDTO>> GetVehicleModelTypeByCompany(string company)
        {
            var VehicleModelType = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleMakeDTO>>($"api/VehicleMake/GetVehicleModelByCompany/" + company);
            return VehicleModelType;
        }
        public async Task Create(CreateVehicleMakeDTO createVehicleModelTypeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/VehicleMake/Create", createVehicleModelTypeDTO);
            var response = await result.Content.ReadFromJsonAsync<VehicleMakeDTO>();
            vehicleModelType = response;
        }

    }
}
