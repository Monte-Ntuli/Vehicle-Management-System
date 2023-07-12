using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using BlazorApp1.Shared.VehiclesDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp1.Client.Pages.Schedules
{
    public class SchedulesBase : ComponentBase
    {
        [Parameter]
        public string company { get; set; }

        [Parameter]
        public int ID { get; set; }

        [Inject]
        public IVehicleService VehicleService { get; set; }

        [Inject]
        public IVehicleTypeService vehicleTypeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IVehicleMakeService vehicleModelTypeService { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
        public IEnumerable<VehicleTypeDTO> VehicleTypes { get; set; } = new List<VehicleTypeDTO>();
        public IEnumerable<VehicleMakeDTO> VehicleModelTypes { get; set; } = new List<VehicleMakeDTO>();
        public IEnumerable<EmployeeDTO> Employee { get; set; } = new List<EmployeeDTO>();

        //public ScheduleDTO Schedule { get; set; } = new ScheduleDTO();
        protected override async Task OnInitializedAsync()
        {
            
        }
    }
}
