using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class QuestionaireService : IQuestionaireService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        public List<QuestionaireDTORM> questionaires { get; set; } = new List<QuestionaireDTORM>();
        public List<CreateQuestionaireDTO> creates { get; set; } = new List<CreateQuestionaireDTO>();
        public List<QuestionsDTO> questions { get; set; } = new List<QuestionsDTO>();

        public QuestionaireService(HttpClient httpClient, IJSRuntime JSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = JSRuntime;
        }
        public async Task CreateQuestion(CreateQuestionDTO createQuestion)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Questions/Create", createQuestion);
            var response = await result.Content.ReadFromJsonAsync<List<QuestionsDTO>>();
            questions = response;
        }
        public async Task<IEnumerable<QuestionsDTO>> GetQuestionsByCompany(string company)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionsDTO>>($"api/Questions/GetQuestionByCompany/" + company);
            return result;
        }
        public async Task<IEnumerable<QuestionsDTO>> GetQuestionByQuestionaire(int ID)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionsDTO>>($"api/Questions/GetQuestionByQuestionare/" + ID);
            return result;
        }
        public async Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByID(int QuestionaireID)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionaireDTORM>>($"api/Questionaire/GetQuestionaireByID/" + QuestionaireID);
            return result;
        }
        public async Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByCompany(string company)
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
                await _jsRuntime.InvokeVoidAsync("alert", "Questionaire created successfuly");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("alert", "Server error occured, please check internet connection");
            }

        }
    }
}
