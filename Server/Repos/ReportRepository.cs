using BlazorApp1.Client.Repos;
using BlazorApp1.Server.Entities;
using BlazorApp1.Server.Repos.Interfaces;

namespace BlazorApp1.Server.Repos
{
    public class ReportRepository : Repository<ReportEntity>, IReportRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;

        public ReportRepository(VehicleDbContext context) : base(context)
        {

        }
    }
}
