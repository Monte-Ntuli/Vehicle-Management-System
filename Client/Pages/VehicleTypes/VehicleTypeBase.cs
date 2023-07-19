using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorApp1.Client.Pages.VehicleTypes
{
    public class VehicleTypeBase : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Parameter]
        public string company { get; set; }

        [Parameter]
        public int ID { get; set; }

        [Inject]
        public IVehicleTypeService vehicleTypeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }

        [Inject]
        public IQuestionaireService QuestionaireService { get; set; }

        public IEnumerable<VehicleTypeDTO> VehicleTypes { get; set; } = new List<VehicleTypeDTO>();
        public IEnumerable<QuestionaireDTORM> Questionaires { get; set; } = new List<QuestionaireDTORM>();
        public IEnumerable<QuestionaireDTORM> Questionare { get; set; } = new List<QuestionaireDTORM>();

        public CreateVehicleTypeDTO CreateVehicleType = new CreateVehicleTypeDTO();
        public UpdateVehicleTypeDTO updateVehicleType { get; set; } = new UpdateVehicleTypeDTO();
        public LoginDTO login { get; set; } = new LoginDTO();
        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            VehicleTypes = await vehicleTypeService.GetVehicleTypeByCompany(company);
            Questionaires = await QuestionaireService.GetQuestionaireByCompany(company);
        }

        public async void AddVehicleType()
        {
            NavMan.NavigateTo("AddVehicleType");
        }
        public async void CreateVehicle()
        {
            NavMan.NavigateTo("AddVehicle");
        }

        public async void SaveVehicleType()
        {
            CreateVehicleType.Company = company;
            await vehicleTypeService.Create(CreateVehicleType);
            Snackbar.Add("Vehicle Type Added Successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
        }

        public async Task UpdateVehicleType()
        {
            await vehicleTypeService.Update(updateVehicleType);
        }
        public async Task viewVehicleType(int id)
        {
            NavMan.NavigateTo($"ViewVehicleType/{id}");
        }
        public async Task editVehicleType(int id)
        {
            NavMan.NavigateTo($"EditVehicleType/{id}");
        }
    }
}
