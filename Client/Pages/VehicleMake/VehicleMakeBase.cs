using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorApp1.Client.Pages.VehicleMake
{
    public class VehicleMakeBase : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Parameter]
        public string company { get; set; }

        [Parameter]
        public int ID { get; set; }

        [Inject]
        IVehicleMakeService vehicleModelTypeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }
        public LoginDTO login { get; set; } = new LoginDTO();
        public IEnumerable<VehicleMakeDTO> VehicleModelTypes { get; set; } = new List<VehicleMakeDTO>();
        public UpdateVehicleMakeDTO updateVehicleModelTypes { get; set; } = new UpdateVehicleMakeDTO();
        public CreateVehicleMakeDTO createVehicleModelType { get; set; } = new CreateVehicleMakeDTO();
        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            VehicleModelTypes = await vehicleModelTypeService.GetVehicleModelTypeByCompany(company);
        }

        public async Task viewVehicleModelType(int id)
        {
            NavMan.NavigateTo($"ViewVehicleModelType/{id}");
        }
        public async Task editVehicleModelType(int id)
        {
            NavMan.NavigateTo($"EditVehicleModelType/{id}");
        }

        public async Task navigateToAdd()
        {
            NavMan.NavigateTo("AddVehicleModelType");
        }

        public async Task SaveVehicleModelType()
        {
            createVehicleModelType.Company = company;
            await vehicleModelTypeService.Create(createVehicleModelType);
            Snackbar.Add("Vehicle Model Type Added Successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
        }
        public async void CreateVehicle()
        {
            NavMan.NavigateTo("AddVehicle");
        }
    }
}
