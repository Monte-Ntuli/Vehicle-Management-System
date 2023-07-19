using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Components.Forms;
using System.Data;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.VehiclesDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using BlazorApp1.Shared.AppUserDTO;
using MudBlazor;

namespace BlazorApp1.Client.Pages.Vehicles
{
    public class VehiclesBase : ComponentBase
    {

        public DataTable dataTable = new DataTable();
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
        public ISnackbar Snackbar { get; set; }

        public IEnumerable<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
        public IEnumerable<VehicleTypeDTO> VehicleTypes { get; set; } = new List<VehicleTypeDTO>();
        public IEnumerable<VehicleMakeDTO> VehicleModelTypes { get; set; } = new List<VehicleMakeDTO>();
        public IEnumerable<VehicleDTO> Vehicle { get; set; } = new List<VehicleDTO>();
        public HttpResponseMessage ApiResult { get; set; }

        public CreateVehicleDTO CreateVehicle = new CreateVehicleDTO();

        public UpdateVehicleDTO updateVehicle = new UpdateVehicleDTO();
        public LoginDTO login { get; set; } = new LoginDTO();
        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            Vehicles = await VehicleService.GetVehicleByCompany(company);
            VehicleTypes = await vehicleTypeService.GetVehicleTypeByCompany(company);
            VehicleModelTypes = await vehicleModelTypeService.GetVehicleModelTypeByCompany(company);
        }

        protected override async Task OnParametersSetAsync()
        {
            Vehicle = await VehicleService.GetVehicleByID(ID);
        }
        public async Task SaveVehicle()
        {
            CreateVehicle.Company = company;
            if (CreateVehicle.VehicleModelType == 0)
            {
                Snackbar.Add("Please select Vehicle Make", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (CreateVehicle.VehicleTypeID == 0)
            {
                Snackbar.Add("Please select Vehicle Model", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (CreateVehicle.VehicleModelType == 0 || CreateVehicle.VehicleTypeID == 0 || string.IsNullOrEmpty(CreateVehicle.VehicleReg) || string.IsNullOrEmpty(CreateVehicle.VinNumber))
            {
                Snackbar.Add("Please make sure all values are filled", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                await VehicleService.Create(CreateVehicle);
                Snackbar.Add("Vehicle added successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
        }

        public async Task AddVehicleMake()
        {
            if(VehicleModelTypes.Count() == 0)
            {
                Snackbar.Add("Please Create Vehicle Make", Severity.Warning, config => { config.ShowCloseIcon = false; });
                NavMan.NavigateTo("AddVehicleModelType");
            }
            
        }
        public async Task AddVehicleModel()
        {
            if (VehicleTypes.Count() == 0)
            {
                Snackbar.Add("Please Create Vehicle Model", Severity.Warning, config => { config.ShowCloseIcon = false; });
                NavMan.NavigateTo("AddVehicleType");
            }
        }
        #region update vehicle
        public async Task UpdateVehicle()
        {
            updateVehicle.VehicleID = ID;
            await VehicleService.Update(updateVehicle);
        }
        #endregion

        #region view vehicle
        public async Task viewVehicle(int id)
        {
            NavMan.NavigateTo($"ViewVehicle/{id}");
        }
        #endregion

        #region edit vehicle
        public async Task editVehicle(int id)
        {
            NavMan.NavigateTo($"EditVehicle/{id}");
        }
        #endregion

        #region read data from an excel file
        public async Task ImportExcelFile(InputFileChangeEventArgs e)
        {
            var fileStream = e.File.OpenReadStream();
            var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            fileStream.Close();
            ms.Position = 0;

            ISheet sheet;
            var xxswb = new XSSFWorkbook(ms);

            sheet = xxswb.GetSheetAt(0);
            IRow headerRow = sheet.GetRow(0);
            var RowList = new List<string>();

            int cellCount = headerRow.LastCellNum;

            for (var j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dataTable.Columns.Add(cell.ToString());
            }

            for (var j = (sheet.FirstRowNum + 1); j <= sheet.LastRowNum; j++)
            {
                var row = sheet.GetRow(j);
                for (var i = row.FirstCellNum; i < cellCount; i++)
                {
                    RowList.Add(row.GetCell(i).ToString());
                }
                if (RowList.Count > 0)
                {
                    dataTable.Rows.Add(RowList.ToArray());
                }
                RowList.Clear();
            }

        }
        public async Task Import()
        {
            
        }
        
        #endregion

       #region export data to an excel file
       public async Task export()
       {
            ExportToExcel();
       }

        private void ExportToExcel()
        {
            // Get the list of data to export
            var data = Vehicles;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Create a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add the headers to the worksheet
                worksheet.Cells[1, 1].Value = "Vehicle Registration";
                worksheet.Cells[1, 1].Value = "Vin Number";
                worksheet.Cells[1, 1].Value = "Vehicle Type";
                worksheet.Cells[1, 1].Value = "Vehicle Model";


                var headers = new string[] { "Vehicle Registration", "Vin Number", "Vehicle Type", "Vehicle Model" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Add the data to the worksheet
                for (int i = 0; i < data.Count(); i++)
                {

                    worksheet.Cells[i + 2, 1].Value = data.ElementAt(i).VehicleReg;
                    worksheet.Cells[i + 2, 2].Value = data.ElementAt(i).VinNumber;

                    // display text instead of number
                    foreach (var item in VehicleTypes)
                    {
                        if (item.VehicleTypeID == data.ElementAt(i).VehicleTypeID)
                        {
                            worksheet.Cells[i + 2, 3].Value = item.VehicleTypeTitle;
                        }
                    }

                    // display text instead of number
                    foreach (var item in VehicleModelTypes)
                    {
                        if (item.VehicleMakeID == data.ElementAt(i).VehicleModelType)
                        {
                            worksheet.Cells[i + 2, 4].Value = item.VehicleMakeTitle;
                        }
                    }


                }

                // Set the column widths
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;


                // Save the Excel package to a stream
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);

                    // Download the Excel file
                    var content = stream.ToArray();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = "export.xlsx";
                    NavMan.NavigateTo($"data:{contentType};base64,{Convert.ToBase64String(content)}", true);
                }
            }
        }
        #endregion

    }

}
