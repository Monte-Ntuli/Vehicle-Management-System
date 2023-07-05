using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Client.Repos
{
    public class QuestionaireRepository : Repository<QuestionaireEntity>, IQuestionaireRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;
        public QuestionaireRepository(VehicleDbContext context) : base(context) 
        {

        }
        public async Task<List<QuestionaireEntity>> GetQuestionaireByCompanyTestAsync()
        {
            var questionaires = await _dbContext.Questionaires.Where(x => x.Company == "MyCompany").ToListAsync();
            if (questionaires == null) { return null; }
            else { return questionaires.ToList(); }
        }
        public async Task<List<QuestionaireEntity>> GetQuestionaireByVehicleTypeAsync(int vehicleTypeID)
        {
            var questionaire = await _dbContext.Questionaires.Where(x => x.VehicleTypeID == vehicleTypeID).ToListAsync();
            if (questionaire == null) { return null; }
            else { return questionaire; }
        }
        public async Task<bool> RestoreQuestionaireAsync(int QuestionaireID)
        {
            var questionaire = await _dbContext.Questionaires.FirstOrDefaultAsync(x => x.QuestionaireID == QuestionaireID);
            if (questionaire != null)
            {
                questionaire.isDeleted = false;
                _dbContext.Update(questionaire);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteQuestionaireAsync(int QuestionaireID)
        {
            var questionaire = await _dbContext.Questionaires.FirstOrDefaultAsync(x => x.QuestionaireID == QuestionaireID);
            if (questionaire != null)
            {
                questionaire.isDeleted = true;
                _dbContext.Update(questionaire);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<List<QuestionaireEntity>> GetQuestionaireByCompanyAsync(string Company)
        {
            var questionaire = await _dbContext.Questionaires.Where(x => x.Company == Company).ToListAsync();
            if (questionaire == null) { return null; }
            else { return questionaire; }
        }
        public async Task<List<QuestionaireEntity>> GetQuestionaireByIDAsync(int QuestionaireID)
        {
            var questionaire = await _dbContext.Questionaires.Where(x => x.QuestionaireID == QuestionaireID).ToListAsync();
            if (questionaire == null) { return null; }
            else { return questionaire; }
        }
        public async Task<QuestionaireEntity> UpdateAsync(QuestionaireEntity entity)
        {
            var questionaire = await _dbContext.Questionaires.FirstOrDefaultAsync(x => x.QuestionaireID == entity.QuestionaireID);

            if (questionaire == null)
            {
                return null;
            }

            questionaire.Title = entity.Title;
            questionaire.VehicleTypeID = entity.VehicleTypeID;

            _dbContext.Update(questionaire);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async override Task<QuestionaireEntity> AddAsync(QuestionaireEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.QuestionaireID = GenerateQuestionaireID();
            entity.isDeleted = false;

            var vehicleType = _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == entity.VehicleTypeID);

            if (vehicleType.Result.hasQuestionaire == false)
            {
                vehicleType.Result.hasQuestionaire = true;
                vehicleType.Result.QuestionaireID = entity.QuestionaireID;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            if (vehicleType.Result.hasQuestionaire == true)
            {
                return null;
            }
            else
            {
                return null;
            }
            
        }
        private int GenerateQuestionaireID()
        {
            var highestId = _dbContext.Questionaires.OrderByDescending(x => x.QuestionaireID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.QuestionaireID + 1;
            }
            return 1;
        }
    }
}
