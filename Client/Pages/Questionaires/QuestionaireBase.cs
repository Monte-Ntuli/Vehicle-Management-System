using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;
using BlazorApp1.Shared.VehicleTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using OfficeOpenXml;

namespace BlazorApp1.Client.Pages.Questionaires
{
    public class QuestionaireBase:ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

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
        public NavigationManager NavMan { get; set; }
        public IEnumerable<QuestionaireDTORM> Questionaires { get; set; } = new List<QuestionaireDTORM>();
        public IEnumerable<QuestionsDTO> Questions { get; set; } = new List<QuestionsDTO>();
        public IEnumerable<QuestionaireDTORM> Questionare { get; set; } = new List<QuestionaireDTORM>();
        public IEnumerable<QuestionsDTO> Question { get; set; } = new List<QuestionsDTO>();
        public IEnumerable<VehicleTypeDTO> VehicleTypes { get; set; } = new List<VehicleTypeDTO>();

        public CreateQuestionaireDTO createQuestionaireDTO = new CreateQuestionaireDTO();
        
        public CreateQuestionDTO createQuestionDTO = new CreateQuestionDTO();

        public UpdateQuestionaireDTO updateQuestionaireDTO = new UpdateQuestionaireDTO();

        public UpdateQuestionDTO updateQuestionDTO = new UpdateQuestionDTO();
        public LoginDTO login { get; set; } = new LoginDTO();
        public List<QuestionsDTO> question { get; set; } = new List<QuestionsDTO>();

        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            Questionaires = await QuestionaireService.GetQuestionairesByCompany(company);
            VehicleTypes = await vehicleTypeService.GetVehicleTypeByCompany(company);
            Questions = await QuestionaireService.GetQuestionsByQuestionaire(ID);
        }
        public async Task CreateQuestion()
        {
            createQuestionDTO.Company = company;
            createQuestionDTO.QuestionaireID = ID;
            await QuestionaireService.CreateQuestion(createQuestionDTO);
            Snackbar.Add("Question Added Successfullyy", Severity.Success, config => { config.ShowCloseIcon = false; });
        }
        public async Task SaveQuestionaire()
        {
            createQuestionaireDTO.Company = company;
            if (string.IsNullOrEmpty(createQuestionaireDTO.Title))
            {
                Snackbar.Add("Please add title to continue", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (createQuestionaireDTO.VehicleTypeID == 0)
            {
                Snackbar.Add("Please add vehicle type to continue", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                await QuestionaireService.CreateQuestionaire(createQuestionaireDTO);
                Snackbar.Add("Questionaire Created Successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
        }

        public async Task AddVehicleType()
        {
            if (VehicleTypes.Count() == 0)
            {
                Snackbar.Add("Please Create Vehicle Model", Severity.Warning, config => { config.ShowCloseIcon = false; });
                NavMan.NavigateTo("AddVehicleType");
            }
            
        }
        protected override async Task OnParametersSetAsync()
        {
            Questionare = await QuestionaireService.GetQuestionaireByID(ID);
            Questions = await QuestionaireService.GetQuestionsByQuestionaire(ID);
        }

        public async Task viewQuestionaire(int QuestionaireID)
        {
            NavMan.NavigateTo($"ViewQuestionaire/{QuestionaireID}");
        }
        public async Task editQuestionaire(int QuestionaireID)
        {
            NavMan.NavigateTo($"EditQuestionaire/{QuestionaireID}");
        }
        public async Task editQuestion(int QuestionID)
        {
            NavMan.NavigateTo($"EditQuestion/{QuestionID}");
        }

        #region export data to an excel file
        public async Task export()
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            // Get the list of data to export
            var data = Questionaires;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Create a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add the headers to the worksheet
                worksheet.Cells[1, 1].Value = "Questionare Title";
                worksheet.Cells[1, 1].Value = "Vehicle Type";


                var headers = new string[] { "Questionare Title", "Vehicle Type"};
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Add the data to the worksheet
                for (int i = 0; i < data.Count(); i++)
                {

                    worksheet.Cells[i + 2, 1].Value = data.ElementAt(i).Title;

                    // display text instead of number
                    foreach (var item in VehicleTypes)
                    {
                        if (item.VehicleTypeID == data.ElementAt(i).VehicleTypeID)
                        {
                            worksheet.Cells[i + 2, 2].Value = item.VehicleTypeTitle;
                        }
                    }

                }

                // Set the column widths
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;


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
