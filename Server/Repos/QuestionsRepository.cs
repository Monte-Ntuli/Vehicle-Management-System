using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Client.Repos
{
    public class QuestionsRepository : Repository<QuestionsEntity>, IQuestionsRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public QuestionsRepository(VehicleDbContext context) : base(context)
        {

        }
        public async Task<List<QuestionsEntity>> GetQuestionByCompanyAsync(string company)
        {
            var questions = await _dbContext.Questions.Where(x => x.Company == company).ToListAsync();
            if (questions == null) { return null; }
            else { return questions.ToList(); }
        }
        public async Task<List<QuestionsEntity>> GetQuestionsByCompanyTestAsync()
        {
            var questions = await _dbContext.Questions.Where(x => x.QuestionaireID == 2 ).ToListAsync();
            if (questions == null) { return null; }
            else { return questions.ToList(); }
        }
        public async Task<QuestionsEntity> UpdateAsync(QuestionsEntity entity)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionID == entity.QuestionID);

            if (question == null)
            {
                return null;
            }

            question.Title = entity.Title;

            _dbContext.Update(question);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<QuestionsEntity>> GetQuestionByQuestionareAsync(int questionaireID)
        {
            var question = await _dbContext.Questions.Where(x => x.QuestionaireID == questionaireID).ToListAsync();
            if (question == null) { return null; }
            else { return question; }
        }
        public async Task<List<QuestionsEntity>> GetQuestionByIDAsync(int QuestionID)
        {
            var question = await _dbContext.Questions.Where(x => x.QuestionID == QuestionID).ToListAsync();
            if (question == null) { return null; }
            else { return question; }
        }
        public async Task<bool> DeleteQuestionAsync(int QuestionID)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionID == QuestionID);
            if (question != null)
            {
                question.isDeleted = true;
                _dbContext.Update(question);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RestoreQuestionAsync(int QuestionID)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionID == QuestionID);
            if (question != null)
            {
                question.isDeleted = false;
                _dbContext.Update(question);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async override Task AddAsync(QuestionsEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.QuestionID = GenerateQuestionTypeID();
            entity.isDeleted = false;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        private int GenerateQuestionTypeID()
        {
            var highestId = _dbContext.Questions.OrderByDescending(x => x.QuestionID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.QuestionID + 1;
            }
            return 1;
        }
    }
}
