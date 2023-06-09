using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.Contracts.Persistence
{
    public interface IAndroidWorkerRepository : IGenericRepository<AndroidWorker>
    {
        Task<List<AndroidWorker>> GetAndroidWorkersByActivities();
        Task<IEnumerable<Activity>> GetActivitiesByWorkerIdAsync(char workedId);        
    }
}
