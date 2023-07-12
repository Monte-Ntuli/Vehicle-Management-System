using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
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
            var response = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
            employee = response;

        }

        public async Task Update(UpdateEmployeeDTO updateEmployeeDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Employee/Update", updateEmployeeDTO);
            var response = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
            employee = response;
        }
    }
}
