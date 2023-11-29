using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using BlazorApp1.Shared.VehiclesDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using MathNet.Numerics.RootFinding;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp1.Client.Pages.Reports
{
    public class ReportsBase : ComponentBase
    {
        [Parameter]
        public int ID { get; set; }

        [Parameter]
        public string company { get; set; }
        [Inject]
        public IQuestionaireService QuestionaireService { get; set; }

        [Inject]
        public IVehicleTypeService vehicleTypeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IVehicleService VehicleService { get; set; }

        [Inject]
        public IVehicleMakeService vehicleModelTypeService { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }
        public IEnumerable<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
        public IEnumerable<QuestionaireDTORM> Questionaires { get; set; } = new List<QuestionaireDTORM>();
        public IEnumerable<QuestionaireDTORM> Questionare { get; set; } = new List<QuestionaireDTORM>();
        public IEnumerable<VehicleTypeDTO> VehicleTypes { get; set; } = new List<VehicleTypeDTO>();
        public IEnumerable<VehicleMakeDTO> VehicleModelTypes { get; set; } = new List<VehicleMakeDTO>();
        public LoginDTO login { get; set; } = new LoginDTO();

        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            Questionaires = await QuestionaireService.GetQuestionairesByCompany(company);
            VehicleTypes = await vehicleTypeService.GetVehicleTypeByCompany(company);
            Vehicles = await VehicleService.GetVehicleByCompany(company);
            VehicleModelTypes = await vehicleModelTypeService.GetVehicleModelTypeByCompany(company);
        }
        protected override async Task OnParametersSetAsync()
        {
            Questionare = await QuestionaireService.GetQuestionaireByID(ID);
            
        }
        public async Task viewReportVehicle(int VehicleTypeID)
        {
            NavMan.NavigateTo($"ViewReportsVehicle/{VehicleTypeID}");
        }

        public async Task viewVehicleReport(int vehicleID)
        {
            NavMan.NavigateTo($"ViewVehicleReports/{vehicleID}");
        }
        
    }
}
