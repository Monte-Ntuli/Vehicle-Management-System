using BlazorApp1.Shared.QuestionsDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionsDTO>> GetQuestionsByCompany(string company);
    }
}
