using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.AppUserDTO;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> AddAsync(EmployeeEntity entity);
        Task<List<EmployeeEntity>> GetEmployeeByIDAsync(int employee);
        Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity);
        Task<bool> DeleteEmployeeAsync(int employee);
        Task<bool> RestoreEmployeeAsync(int employee);
        //Task<string> Login(LoginUserRM userEntity);
        Task<EmployeeEntity> Login(LoginDTO userEntity);
        Task<List<EmployeeEntity>> GetEmployeeByEmailAsync(string email);
        Task<List<EmployeeEntity>> GetEmployeeByCompanyAsync(string company);
        Task<List<EmployeeEntity>> GetEmployeeByRoleAsync(string role);
    }
}
