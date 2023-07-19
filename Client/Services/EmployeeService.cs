using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private readonly HttpClient _httpClient;
        public List<EmployeeDTO> employee { get; set; } = new List<EmployeeDTO>();
        public List<CreateEmployeeDTO> creates { get; set; } = new List<CreateEmployeeDTO>();
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompany(string company)
        {
            var employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDTO>>($"api/Employee/GetEmployeeByCompany/" + company);
            return employees;

        }
        public async Task Create(CreateEmployeeDTO createEmployeeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Employee/Register", createEmployeeDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
                Snackbar.Add("Employee AddedS sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                employee = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }

        }

        public async Task Update(UpdateEmployeeDTO updateEmployeeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Employee/Update", updateEmployeeDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
                Snackbar.Add("Employee Updated sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                employee = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
    }
}
