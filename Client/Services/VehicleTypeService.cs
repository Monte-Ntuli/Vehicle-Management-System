using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.VehiclesDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }
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
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<List<VehicleTypeDTO>>();
                Snackbar.Add("Vehicle Model created sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                vehicletype = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }

        }

        public async Task Update(UpdateVehicleTypeDTO updateVehicleTypeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/VehicleType/Update", updateVehicleTypeDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<List<UpdateVehicleTypeDTO>>();
                Snackbar.Add("Vehicle Model created sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                update = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
    }
}
