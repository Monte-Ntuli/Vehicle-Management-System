using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp1.Client.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        public string Company;
        public string UserName { get; set; }
        public AppUserDTO AppUser { get; set; } = new AppUserDTO();
        public IEnumerable<AppUserDTO> user { get; set; } = new List<AppUserDTO>();
        protected override async Task OnInitializedAsync()
        {

        }
        public async Task SetCompanyFromLocalStorage()
        {
            //await JS.InvokeVoidAsync("localstorage.setItem", "Company", Company);
            await localStorage.SetItemAsync("Company", Company);
        }

        public async Task GetCompanyFromLocalStorage()
        {
            //var localStorageCompany = await JS.InvokeAsync<string>("localstorage.getItem","Company");
            var localStorageCompany = await localStorage.GetItemAsync<string>("Company");
        }
    }
}
