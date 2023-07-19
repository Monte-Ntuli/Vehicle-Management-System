using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class AccountService : IAccountService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navMan;
        private readonly ILocalStorageService _localStorage;
        public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public List<AppUserDTO> AppUsers { get; set; } = new List<AppUserDTO>();
        public List<LoginDTO> LoginUsers { get; set; } = new List<LoginDTO>();
        public List<char> ApiLoginResponse { get; set; } = new List<char>();
        public HttpResponseMessage ApiResponse { get; set; } = new HttpResponseMessage();
        public AccountService(HttpClient httpClient, NavigationManager NavMan, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _navMan = NavMan;
            _localStorage = localStorage;
        }

        #region change password
        public async Task ChangePassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/ChangePassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                Snackbar.Add("Password changed sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("EditProfile", true);
            }
        }
        #endregion

        #region forgot password
        public async Task ForgotPassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/ForgotPassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                Snackbar.Add("Password reset successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("/", true);
            }
            else
            {
                Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("ForgotPasswordChange", true);
            }
        }

        public async Task FindEmail(FindEmailDTO findEmailDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/FindEmail", findEmailDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _navMan.NavigateTo("ForgotPasswordChange", true);
                await _localStorage.SetItemAsync("EmailForgotPassWord", findEmailDTO);
            }
            else
            {
                Snackbar.Add("Email has been sent with directions to update password", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
        }
        #endregion

        #region Register new account
        public async Task Register(EmployeeDTO appUser)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/Register", appUser);

            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {
                Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.Accepted)
            {
                var data = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
                _navMan.NavigateTo("/");
            }
            Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
        }
        #endregion

        #region Login
        public async Task Login(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/Login", loginDTO);
            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {
                Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                Snackbar.Add(response.ToString() + " Please Confirm Email Address ", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _navMan.NavigateTo("Dashboard", true);
                //await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "UserName", loginDTO.Email);
                await _localStorage.SetItemAsync("UserName", loginDTO.Email);
            }

        }
        #endregion

        #region Get Admin Details
        public async Task<AppUserDTO> GetUserByEmailTest(string email)
        {
            var result = await _httpClient.GetFromJsonAsync<AppUserDTO>($"api/AppUser/GetUserByEmail/" + email);
            return result;
        }
        #endregion


    }
}
