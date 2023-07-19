using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
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

        [Inject]
        public ISnackbar Snackbar { get; set; }
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
            ValidatePassword(employee.Password);

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
                Snackbar.Add("Failed to register", Severity.Warning, config => { config.ShowCloseIcon = false; });
                NavMan.NavigateTo("Register");
            }

        }

        private void ValidatePassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            Match match = regex.Match(password);

            if (match.Success)
            {
                checker = true;
            }
            else
            {
                Snackbar.Add("Password must contain: 8 Characters, 1 LowerCase, 1 UpperCase, 1 number and 1 Special Character", Severity.Warning, config => { config.ShowCloseIcon = false; });
                checker = false;
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
                Snackbar.Add("Please type in valid email", Severity.Warning, config => { config.ShowCloseIcon = false; });
                checker = false;
            }
        }
        public async Task Validate()
        {
            if (confirmEmail != employee.Email)
            {
                Snackbar.Add("Please make sure emails match", Severity.Warning, config => { config.ShowCloseIcon = false; });
                checker = false;
            }
            if (confirmPassword != employee.Password)
            {
                Snackbar.Add("Please make sure passwords match", Severity.Warning, config => { config.ShowCloseIcon = false; });
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

            if (string.IsNullOrEmpty(login.Email))
            {
                Snackbar.Add("Email can not be empty", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if(string.IsNullOrEmpty(login.Password))
            {
                Snackbar.Add("Password can not be empty", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                var result = AccountService.Login(login);
                company = await localStorage.GetItemAsync<string>("UserName");
            }
            

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
                Snackbar.Add("Please make sure passwords match", Severity.Warning, config => { config.ShowCloseIcon = false; });
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
                    Snackbar.Add("Password is not strong enough", Severity.Warning, config => { config.ShowCloseIcon = false; });
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
