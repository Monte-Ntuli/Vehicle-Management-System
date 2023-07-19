using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

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
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<VehicleMakeDTO>();
                Snackbar.Add("Vehicle Make Created sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                vehicleModelType = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }

    }
}
