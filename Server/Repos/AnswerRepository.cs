using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Client.Repos;
using BlazorApp1.Server.Entities;
using BlazorApp1.Server.Repos.Interfaces;

namespace BlazorApp1.Server.Repos
{
    public class AnswerRepository : Repository<AnswerEntity>, IAnswerRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public AnswerRepository(VehicleDbContext context) : base(context)
        {

        }
    }
}
