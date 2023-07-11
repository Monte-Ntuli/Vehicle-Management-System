
using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BlazorApp1.Client.Repos
{
    public class VehicleMakeRepository : Repository<VehicleMakeEntity>, IVehicleMakeRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public VehicleMakeRepository(VehicleDbContext context) :base(context)
        {

        }

        public async Task<List<VehicleMakeEntity>> GetVehicleByCompanyTestAsync()
        {
            var vehiclemodel = await _dbContext.VehicleMake.Where(x => x.Company == "MyCompany").ToListAsync();
            if (vehiclemodel == null) { return null; }
            else { return vehiclemodel.ToList(); }
        }
        public async override Task AddAsync(VehicleMakeEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.VehicleMakeID = GenerateVehicleMakeID();
            entity.isDeleted = false;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        private int GenerateVehicleMakeID()
        {
            var highestId = _dbContext.VehicleMake.OrderByDescending(x => x.VehicleMakeID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.VehicleMakeID + 1;
            }
            return 1;
        }
        public async Task<bool> DeleteVehicleMakeAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleMake.FirstOrDefaultAsync(x => x.VehicleMakeID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = true;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RestoreVehicleMakeAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleMake.FirstOrDefaultAsync(x => x.VehicleMakeID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = false;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<VehicleMakeEntity>> GetVehicleMakeByIDAsync(int VehicleID)
        {
            var vehicle = await _dbContext.VehicleMake.Where(x => x.VehicleMakeID == VehicleID).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }
        public async Task<List<VehicleMakeEntity>> GetVehicleMakeByCompanyAsync(string Company)
        {
            var vehicle = await _dbContext.VehicleMake.Where(x => x.Company == Company).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }

        public async Task<VehicleMakeEntity> UpdateAsync(VehicleMakeEntity entity)
        {
            var vehicle = await _dbContext.VehicleMake.FirstOrDefaultAsync(x => x.VehicleMakeID == entity.VehicleMakeID);

            if (vehicle == null)
            {
                return null;
            }

            vehicle.VehicleMakeTitle = entity.VehicleMakeTitle;

            _dbContext.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }
    }
}
