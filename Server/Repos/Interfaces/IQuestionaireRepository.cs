using BlazorApp1.Server.Entities;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IQuestionaireRepository
    {
        Task<QuestionaireEntity> AddAsync(QuestionaireEntity entity);
        Task<QuestionaireEntity> UpdateAsync(QuestionaireEntity entity);
        Task<List<QuestionaireEntity>> GetQuestionaireByIDAsync(int questionaire);
        Task<bool> RestoreQuestionaireAsync(int QuestionaireID);
        Task<bool> DeleteQuestionaireAsync(int QuestionaireID);
        Task<List<QuestionaireEntity>> GetQuestionaireByCompanyAsync(string Company);
        Task<List<QuestionaireEntity>> GetQuestionaireByVehicleTypeAsync(int vehicleTypeID);
        Task<List<QuestionaireEntity>> GetQuestionaireByCompanyTestAsync();
    }
}
