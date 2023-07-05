using BlazorApp1.Server.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Client.Repos.Interfaces
{
    public interface IQuestionsRepository
    {
        Task AddAsync(QuestionsEntity entity);
        Task<bool> RestoreQuestionAsync(int QuestionID);
        Task<bool> DeleteQuestionAsync(int QuestionID);
        Task<List<QuestionsEntity>> GetQuestionByIDAsync(int QuestionID);
        Task<List<QuestionsEntity>> GetQuestionByQuestionareAsync(int questionaireID);
        Task<List<QuestionsEntity>> GetQuestionByCompanyAsync(string company);
        Task<QuestionsEntity> UpdateAsync(QuestionsEntity entity);
        Task<List<QuestionsEntity>> GetQuestionsByCompanyTestAsync();
    }
}
