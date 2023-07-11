using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Client.Repos;
using BlazorApp1.Server.Entities;

namespace BlazorApp1.Server.Repos.Interfaces
{
    public class AddressRepository : Repository<AddressEntity>, IAddressRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;
        private readonly IConfiguration _config;
        public AddressRepository(VehicleDbContext context, IConfiguration config) : base(context)
        {
            _config = config;
        }
    }
}
