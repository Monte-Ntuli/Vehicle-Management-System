﻿using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using BlazorApp1.Shared.VehiclesDTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class VehicleService : IVehicleService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; } 
        private readonly HttpClient _httpClient;
        public CreateVehicleDTO createVehicle {  get; set; } = new CreateVehicleDTO();
        public List<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
        public VehicleDTO VehicleDTO { get; set; } = new VehicleDTO();
        public CreateVehicleDTO creates { get; set; } = new CreateVehicleDTO();
        public HttpResponseMessage ApiResult { get; set; } = new HttpResponseMessage();
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
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateVehicleDTO>();
                creates = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
        public async Task Update(UpdateVehicleDTO updateVehicleDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Vehicles/Update", updateVehicleDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<List<VehicleDTO>>();
                Snackbar.Add("Vehicle updated sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                Vehicles = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }

    }
}
