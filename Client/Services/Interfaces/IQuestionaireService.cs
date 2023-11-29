using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IQuestionaireService
    {
        Task<IEnumerable<QuestionaireDTORM>> GetQuestionairesByCompany(string company);
        Task CreateQuestionaire(CreateQuestionaireDTO createQuestionaireDTO);
        Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByID(int QuestionaireID);
        Task<IEnumerable<QuestionsDTO>> GetQuestionsByQuestionaire(int ID);
        Task CreateQuestion(CreateQuestionDTO createQuestion);
    }
}
