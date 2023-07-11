using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navMan;
        private readonly IJSRuntime _jsRuntime;
        private readonly ILocalStorageService _localStorage;
        public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public List<AppUserDTO> AppUsers { get; set; } = new List<AppUserDTO>();
        public List<LoginDTO> LoginUsers { get; set; } = new List<LoginDTO>();
        public List<char> ApiLoginResponse { get; set; } = new List<char>();
        public HttpResponseMessage ApiResponse { get; set; } = new HttpResponseMessage();
        public AccountService(HttpClient httpClient, NavigationManager NavMan, IJSRuntime JSRuntime, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _navMan = NavMan;
            _jsRuntime = JSRuntime;
            _localStorage = localStorage;
        }

        #region change password
        public async Task ChangePassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("AppUser/ChangePassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                await _jsRuntime.InvokeVoidAsync("alert", "Password changed sucessfully ");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("alert", response.ToString());
                _navMan.NavigateTo("EditProfile", true);
            }
        }
        #endregion

        #region forgot password
        public async Task ForgotPassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("AppUser/ForgotPassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                await _jsRuntime.InvokeVoidAsync("alert", "Password reset successfully");
                _navMan.NavigateTo("/", true);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("alert", response.ToString());
                _navMan.NavigateTo("ForgotPasswordChange", true);
            }
        }

        public async Task FindEmail(FindEmailDTO findEmailDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("AppUser/FindEmail", findEmailDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _navMan.NavigateTo("ForgotPasswordChange", true);
                await _localStorage.SetItemAsync("EmailForgotPassWord", findEmailDTO);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("Email has been sent with directions to update password", response.ToString());
            }
        }
        #endregion

        #region Register new account
        public async Task Register(EmployeeDTO appUser)
        {
            var result = await _httpClient.PostAsJsonAsync("AppUser/Register", appUser);

            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {
                await _jsRuntime.InvokeVoidAsync("Alert", response.ToString());
            }
            if (response == System.Net.HttpStatusCode.Accepted)
            {
                var data = await result.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
                _navMan.NavigateTo("/");
            }
            await _jsRuntime.InvokeVoidAsync("Alert", response.ToString());
        }
        #endregion

        #region Login
        public async Task Login(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("AppUser/Login", loginDTO);
            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {

                await _jsRuntime.InvokeVoidAsync("alert", response.ToString());
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
            var result = await _httpClient.GetFromJsonAsync<AppUserDTO>($"AppUser/GetUserByEmail/" + email);
            return result;
        }
        #endregion


    }
}
