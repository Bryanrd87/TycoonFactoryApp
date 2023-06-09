using Microsoft.EntityFrameworkCore;
using TycoonFactoryApp.Core.Contracts.Persistence;
using TycoonFactoryApp.Domain;
using TycoonFactoryApp.Persistence.DatabaseContext;

namespace TycoonFactoryApp.Persistence.Repositories
{
    public class AndroidWorkerRepository : GenericRepository<AndroidWorker>, IAndroidWorkerRepository
    {
        private readonly FactoryDbContext _dbContext;
        public AndroidWorkerRepository(FactoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AndroidWorker>> GetAndroidWorkersByActivities()
        {
            return await _dbContext.AndroidWorkers
                .Include(w => w.Activities)
                .ThenInclude(a => a.Activity)
                .ToListAsync();           
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByWorkerIdAsync(char workedId)
        {
            var result = await _dbContext.AndroidWorkers
                .Include(q => q.Activities)
                .ThenInclude(aw => aw.Activity)
                .Where(q => q.Name == workedId).SelectMany(c => c.Activities)
                .Select(e => e.Activity).ToListAsync();
            return result;
        }       
    }
}
