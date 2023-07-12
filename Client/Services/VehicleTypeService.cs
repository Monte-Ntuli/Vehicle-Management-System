using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.VehicleTypeDTO;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly HttpClient _httpClient;
        public List<VehicleTypeDTO> vehicletype { get; set; } = new List<VehicleTypeDTO>();
        public List<CreateVehicleTypeDTO> creates { get; set; } = new List<CreateVehicleTypeDTO>();
        public List<UpdateVehicleTypeDTO> update { get; set; } = new List<UpdateVehicleTypeDTO>();
        public VehicleTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypeByCompany(string company)
        {
            var VehicleType = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleTypeDTO>>($"api/VehicleType/GetVehicleTypeByCompany/" + company);
            return VehicleType;
        }

        public async Task Create(CreateVehicleTypeDTO createVehicleTypeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/VehicleType/Create", createVehicleTypeDTO);
            var response = await result.Content.ReadFromJsonAsync<List<VehicleTypeDTO>>();
            vehicletype = response;

        }

        public async Task Update(UpdateVehicleTypeDTO updateVehicleTypeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/VehicleType/Update", updateVehicleTypeDTO);
            var response = await result.Content.ReadFromJsonAsync<List<UpdateVehicleTypeDTO>>();
            update = response;
        }
    }
}
