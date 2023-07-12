using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.QuestionsDTO;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly HttpClient _httpClient;
        public QuestionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<QuestionsDTO>> GetQuestionsByCompany(string company)
        {
            var Questions = await _httpClient.GetFromJsonAsync<IEnumerable<QuestionsDTO>>($"api/Questions/GetQuestionsByCompanyTest");
            return Questions;
        }
    }
}
