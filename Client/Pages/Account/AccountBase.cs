using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace BlazorApp1.Client.Pages.Account
{
    public class AccountBase : ComponentBase
    {
        [Parameter]
        public string company { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }

        [Inject]
        ILocalStorageService localStorage { get; set; }

        public AppUserDTO AppUser { get; set; } = new AppUserDTO();
        public AppUserDTO UpdateAppUser { get; set; } = new AppUserDTO();
        public LoginDTO login { get; set; } = new LoginDTO();
        public EmployeeDTO employee { get; set; } = new EmployeeDTO();
        public FindEmailDTO findEmailDTO { get; set; } = new FindEmailDTO();
        public IEnumerable<AppUserDTO> user { get; set; } = new List<AppUserDTO>();
        public IEnumerable<AppUserRM> Test { get; set; } = new List<AppUserRM>();

        public string email;

        public string confirmEmail;

        public string confirmPassword;

        public string NewPassword;

        public bool checker = false;

        public bool isEnabled = true;

        #region check current url and get userdata
        protected override async Task OnInitializedAsync()
        {
            await GetCurrentURI();
            //isEnabled = true;
        }

        public async Task GetCurrentURI()
        {
            string currentUrl = NavMan.Uri;

            if (currentUrl.Contains("Profile"))
            {
                email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
                var EmailForgotPassWord = email.Remove(email.Length - 1, 1);
                var Email = EmailForgotPassWord.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
                var username = EmailForgotPassWord.Remove(0, 1);
                AppUser = await AccountService.GetUserByEmailTest(username);
            }
        }
        #endregion

        #region Register new user
        public async Task Register()
        {
            Validate();

            ValidateEmail(employee.Email);

            if (checker == true)
            {
                employee.Role = "Admin";
                employee.Token = "String";
                employee.UserName = employee.Email;
                var response = AccountService.Register(employee);
                NavMan.NavigateTo("/");
            }
            if (checker == false)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Failed to register");
            }

        }

        private void ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (match.Success)
            {
                checker = true;
            }
            else
            {
                JSRuntime.InvokeVoidAsync("alert", "please type in valid email");
                checker = false;
            }
        }
        public async Task Validate()
        {
            if (confirmEmail != employee.Email)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Please make sure emails match");
                checker = false;
            }
            if (confirmPassword != employee.Password)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Please make sure passwords match");
                checker = false;
            }
            else
            {
                checker = true;
            }

        }
        public void GotoRegister()
        {
            NavMan.NavigateTo("Register");
        }
        #endregion

        #region Login
        public async Task LoginProfile()
        {
            await localStorage.ClearAsync();

            var result = AccountService.Login(login);

            company = await localStorage.GetItemAsync<string>("UserName");

        }

        #endregion

        #region Edit Admin Profile
        public async Task GotoEditAdminProfile()
        {
            //NavMan.NavigateTo("EditProfile");

            if (isEnabled == true) { isEnabled = false; }

            if (isEnabled == false) { isEnabled = true; }
        }

        public async Task EditAdminProfile()
        {
            await ValidateUserDatat();


        }

        public async Task ValidateUserDatat()
        {
            if (string.IsNullOrEmpty(UpdateAppUser.FirstName))
            {
                UpdateAppUser.FirstName = AppUser.FirstName;
            }
            if (string.IsNullOrEmpty(UpdateAppUser.LastName))
            {
                UpdateAppUser.LastName = AppUser.LastName;
            }
            if (string.IsNullOrEmpty(UpdateAppUser.PhoneNumber))
            {
                UpdateAppUser.PhoneNumber = AppUser.PhoneNumber;
            }

            if (string.IsNullOrEmpty(UpdateAppUser.Company))
            {
                UpdateAppUser.Company = AppUser.Company;
            }
            if (string.IsNullOrEmpty(UpdateAppUser.UserName))
            {
                UpdateAppUser.UserName = AppUser.UserName;
            }
            if (string.IsNullOrEmpty(UpdateAppUser.UserName))
            {
                UpdateAppUser.UserName = AppUser.UserName;
            }
        }
        #endregion

        #region Forgot password
        public async Task GoToForgotPasswordEmail()
        {
            NavMan.NavigateTo("ForgotPasswordEmail");
        }

        public async Task FindEmail()
        {
            await AccountService.FindEmail(findEmailDTO);
        }

        public async Task ValidatePasswordChange()
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (confirmPassword != NewPassword)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Please make sure passwords match");
                checker = false;
            }
            if (confirmPassword == NewPassword)
            {
                if (validateGuidRegex.IsMatch(NewPassword))
                {
                    checker = true;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Password is not strong enough");
                    checker = false;
                }
            }

        }

        public async Task ChangePassword()
        {
            ValidatePasswordChange();

            if (checker == true)
            {
                email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "EmailForgotPassWord");
                var username = email.Remove(0, 10);
                var EmailForgotPassWord = username.Remove(username.Length - 2, 2);
                var Email = EmailForgotPassWord.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
                login.Password = NewPassword;
                login.Email = Email;
                await AccountService.ForgotPassword(login);
                NavMan.NavigateTo("/");
            }
        }
        #endregion

        #region Change Password
        public async Task GotoChangePassword()
        {
            NavMan.NavigateTo("ChangePassword");
        }

        public async Task ResetPassword()
        {
            await ValidatePasswordChange();

            if (checker == true)
            {
                email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
                var username = email.Remove(0, 1);
                var EmailForgotPassWord = username.Remove(username.Length - 1, 1);
                var Email = EmailForgotPassWord.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
                login.Password = NewPassword;
                login.Email = Email;
                await AccountService.ChangePassword(login);
                NavMan.NavigateTo("EditProfile");
            }
        }
        #endregion
    }
}
