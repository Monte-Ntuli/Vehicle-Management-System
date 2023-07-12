using BlazorApp1.Shared.EmployeeDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompany(string company);
        Task Create(CreateEmployeeDTO createEmployeeDTO);
        Task Update(UpdateEmployeeDTO updateEmployeeDTO);
    }
}
