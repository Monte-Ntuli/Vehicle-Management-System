
using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BlazorApp1.Client.Repos
{
    public class VehicleModelTypeRepository : Repository<VehicleModelTypeEntity>, IVehicleModelTypeRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public VehicleModelTypeRepository(VehicleDbContext context) :base(context)
        {

        }

        public async Task<List<VehicleModelTypeEntity>> GetVehicleByCompanyTestAsync()
        {
            var vehiclemodel = await _dbContext.VehicleModelTypes.Where(x => x.Company == "MyCompany").ToListAsync();
            if (vehiclemodel == null) { return null; }
            else { return vehiclemodel.ToList(); }
        }
        public async override Task AddAsync(VehicleModelTypeEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.VehicleModelTypeID = GenerateVehicleModelTypeID();
            entity.isDeleted = false;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        private int GenerateVehicleModelTypeID()
        {
            var highestId = _dbContext.VehicleModelTypes.OrderByDescending(x => x.VehicleModelTypeID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.VehicleModelTypeID + 1;
            }
            return 1;
        }
        public async Task<bool> DeleteVehicleModelTypeAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleModelTypes.FirstOrDefaultAsync(x => x.VehicleModelTypeID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = true;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RestoreVehicleModelTypeAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleModelTypes.FirstOrDefaultAsync(x => x.VehicleModelTypeID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = false;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<VehicleModelTypeEntity>> GetVehicleModelTypeByIDAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleModelTypes.Where(x => x.VehicleModelTypeID == VehicleID).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }
        public async Task<List<VehicleModelTypeEntity>> GetVehicleModelTypeByCompanyAsync(string Company)
        {
            var vehicle = await _dbContext.VehicleModelTypes.Where(x => x.Company == Company).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }

        public async Task<VehicleModelTypeEntity> UpdateAsync(VehicleModelTypeEntity entity)
        {
            var vehicle = await _dbContext.VehicleModelTypes.FirstOrDefaultAsync(x => x.VehicleModelTypeID == entity.VehicleModelTypeID);

            if (vehicle == null)
            {
                return null;
            }

            vehicle.VehicleModelTitle = entity.VehicleModelTitle;

            _dbContext.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }
    }
}
