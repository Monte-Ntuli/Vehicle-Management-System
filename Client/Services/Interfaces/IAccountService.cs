using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(EmployeeDTO appUser);
        Task ChangePassword(LoginDTO loginDTO);
        Task Login(LoginDTO loginDTO);
        Task FindEmail(FindEmailDTO findEmailDTO);
        Task ForgotPassword(LoginDTO loginDTO);
        Task<AppUserDTO> GetUserByEmailTest(string email);
    }
}
