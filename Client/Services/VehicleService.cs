using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.VehiclesDTO;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public List<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
        public List<CreateVehicleDTO> creates { get; set; } = new List<CreateVehicleDTO>();
        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehicleByCompany(string company)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleDTO>>("api/Vehicles/GetVehicleByCompany/" + company);
            return result;
        }
        public async Task<IEnumerable<VehicleDTO>> GetVehicleByID(int VehicleID)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleDTO>>($"api/Vehicles/GetVehicleByID/" + VehicleID);
            return result;
        }
        public async Task Create(CreateVehicleDTO createVehicleDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Vehicles/Create", createVehicleDTO);
            var response = await result.Content.ReadFromJsonAsync<List<VehicleDTO>>();
            Vehicles = response;
        }
        public async Task Update(UpdateVehicleDTO updateVehicleDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Vehicles/Update", updateVehicleDTO);
            var response = await result.Content.ReadFromJsonAsync<List<VehicleDTO>>();
            Vehicles = response;
        }

    }
}
