using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Client.Repos
{
    public class VehicleRepository : Repository<VehicleEntity>, IVehicleRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public VehicleRepository(VehicleDbContext context) : base(context)
        {
            
        }
        public async Task<List<VehicleEntity>> GetVehicleByEmail(string email)
        {
            var vehicles = await _dbContext.Vehicles.Where(y => y.Company == email).ToListAsync();

            if (vehicles == null) { return null; }
            else
            {
                return vehicles.ToList();
            }
        }
        public async override Task<VehicleEntity> AddAsync(VehicleEntity entity)
        {
            var NewVehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleReg == entity.VehicleReg);

            if (NewVehicle == null)
            {
                NewVehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VinNumber == entity.VinNumber);

                if (NewVehicle == null)
                {
                    entity.Id = Guid.NewGuid();
                    entity.VehicleID = GenerateVehicleID();
                    entity.isDeleted = false;
                    await _dbContext.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                }
                else return NewVehicle;
            }
            return NewVehicle;
        }
        private int GenerateVehicleID()
        {
            var highestId = _dbContext.Vehicles.OrderByDescending(x => x.VehicleID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.VehicleID + 1;
            }
            return 1;
        }

        public async Task<bool> DeleteVehicleAsync(int VehicleID)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = true;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreVehicleAsync(int VehicleID)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == VehicleID);
            if (vehicle != null)
            {
                vehicle.isDeleted = false;
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<VehicleEntity>> GetVehicleByIDAsync(int VehicleID)
        {
            var vehicle = await _dbContext.Vehicles.Where(x => x.VehicleID == VehicleID).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }

        public async Task<List<VehicleEntity>> GetVehicleByRegAsync(string Registration)
        {
            var vehicle = await _dbContext.Vehicles.Where(x => x.VehicleReg == Registration).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }

        public async Task<List<VehicleEntity>> GetVehicleByCompanyAsync(string Company)
        {
            var vehicle = await _dbContext.Vehicles.Where(x => x.Company == Company).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }
        public async Task<List<VehicleEntity>> GetVehicleByTypeAsync(int vehicleType)
        {
            var vehicle = await _dbContext.Vehicles.Where(x => x.VehicleTypeID == vehicleType).ToListAsync();
            if (vehicle == null) { return null; }
            else { return vehicle; }
        }

        public async Task<VehicleEntity> UpdateAsync(VehicleEntity entity)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == entity.VehicleID);

            if (vehicle == null)
            {
                return null;
            }

            vehicle.VehicleReg = entity.VehicleReg;
            vehicle.VehicleTypeID = entity.VehicleTypeID;
            vehicle.VinNumber = entity.VinNumber;
            vehicle.VehicleModelType= entity.VehicleModelType;

            _dbContext.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }
    }
}
