using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IQuestionaireService
    {
        Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByCompany(string company);
        Task<IEnumerable<QuestionsDTO>> GetQuestionsByCompany(string company);
        Task CreateQuestionaire(CreateQuestionaireDTO createQuestionaireDTO);
        Task<IEnumerable<QuestionaireDTORM>> GetQuestionaireByID(int QuestionaireID);
        Task<IEnumerable<QuestionsDTO>> GetQuestionByQuestionaire(int ID);
        Task CreateQuestion(CreateQuestionDTO createQuestion);
    }
}
