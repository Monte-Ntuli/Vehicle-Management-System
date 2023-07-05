using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Client.Repos
{
    public class VehicleTypeRepository : Repository<VehicleTypeEntity>, IVehicleTypeRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public VehicleTypeRepository(VehicleDbContext context) : base(context)
        {

        }
        public async Task<List<VehicleTypeEntity>> GetVehicleTypeByCompanyTestAsync()
        {
            var vehicleType = await _dbContext.VehicleTypes.Where(x => x.Company == "MyCompany").ToListAsync();
            if (vehicleType == null) { return null; }
            else { return vehicleType.ToList(); }
        }
        public async Task<List<VehicleTypeEntity>> GetVehicleTypeByCompanyAsync(string companyName)
        {
            var VehicleType = await _dbContext.VehicleTypes.Where(x => x.Company == companyName).ToListAsync();
            if (VehicleType == null) { return null; }
            else { return VehicleType; }
        }
        public async Task<List<VehicleTypeEntity>> GetVehicleByIDAsync(int VehicleTypeID)
        {
            var VehicleType = await _dbContext.VehicleTypes.Where(x => x.VehicleTypeID == VehicleTypeID).ToListAsync();
            if (VehicleType == null) { return null; }
            else { return VehicleType; }
        }
        public async Task<bool> RestoreVehicleTypeAsync(int vehicleTypeID)
        {
            var vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicleTypeID);
            if (vehicleType != null)
            {
                vehicleType.isDeleted = false;
                _dbContext.Update(vehicleType);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteVehicleTypeAsync(int vehicleTypeID)
        {
            var vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicleTypeID);
            if (vehicleType != null)
            {
                vehicleType.isDeleted = true;
                _dbContext.Update(vehicleType);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<VehicleTypeEntity> UpdateAsync(VehicleTypeEntity entity)
        {
            var vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == entity.VehicleTypeID);

            if (vehicleType == null)
            {
                return null;
            }

            vehicleType.VehicleTypeTitle = entity.VehicleTypeTitle;

            _dbContext.Update(vehicleType);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async override Task AddAsync(VehicleTypeEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.VehicleTypeID = GenerateVehicleTypeID();
            entity.isDeleted = false;
            entity.QuestionaireID = 0;
            entity.hasQuestionaire = false;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        private int GenerateVehicleTypeID()
        {
            var highestId = _dbContext.VehicleTypes.OrderByDescending(x => x.VehicleTypeID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.VehicleTypeID + 1;
            }
            return 1;
        }
    }
}
