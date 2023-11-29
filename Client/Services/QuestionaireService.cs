using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.EmployeeDTO;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class QuestionaireService : IQuestionaireService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }
        private readonly HttpClient _httpClient;
        public List<QuestionaireDTORM> questionaires { get; set; } = new List<QuestionaireDTORM>();
        public List<CreateQuestionaireDTO> creates { get; set; } = new List<CreateQuestionaireDTO>();
        public List<QuestionsDTO> questions { get; set; } = new List<QuestionsDTO>();
        public CreateQuestionDTO createQuestion {  get; set; } = new CreateQuestionDTO();

        public QuestionaireService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateQuestion(CreateQuestionDTO createQuestion)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Questions/Create", createQuestion);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateQuestionDTO>();
                createQuestion = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
        public async Task<IEnumerable<QuestionsDTO>> GetQuestionsByQuestionaire(int ID)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionsDTO>>($"api/Questions/GetQuestionByQuestionare/" + ID);
            return result;
        }
        public async Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByID(int QuestionaireID)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionaireDTORM>>($"api/Questionaire/GetQuestionaireByID/" + QuestionaireID);
            return result;
        }
        public async Task<IEnumerable<QuestionaireDTORM>> GetQuestionairesByCompany(string company)
        {
            var questionaires = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionaireDTORM>>($"api/Questionaire/GetQuestionaireByCompany/" + company);
            return questionaires;
        }
        public async Task CreateQuestionaire(CreateQuestionaireDTO createQuestionaireDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Questionaire/Create", createQuestionaireDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                Snackbar.Add("Questionaire created successfuly", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                Snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }

        }
    }
}
