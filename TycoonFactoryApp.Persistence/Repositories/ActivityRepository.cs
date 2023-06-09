using Microsoft.EntityFrameworkCore;
using TycoonFactoryApp.Core.Contracts.Persistence;
using TycoonFactoryApp.Domain;
using TycoonFactoryApp.Persistence.DatabaseContext;

namespace TycoonFactoryApp.Persistence.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        private readonly FactoryDbContext _dbContext;
        public ActivityRepository(FactoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _dbContext.Activities
                .Include(a => a.AndroidWorkers)
                .ThenInclude(a => a.Worker)
                .ToListAsync();
        }

        public override async Task<Activity> GetByIdAsync(int id)
        {
            return await _dbContext.Activities
                .Include(a => a.AndroidWorkers)
                .ThenInclude(a => a.Worker)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task RemoveActivity(int id)
        {
            var actvityToRemove = await _dbContext.Activities.FindAsync(id);
            if (actvityToRemove != null)
            {
                _dbContext.Activities.Remove(actvityToRemove);
                await _dbContext.SaveChangesAsync();
            }           
        }
    }
}
