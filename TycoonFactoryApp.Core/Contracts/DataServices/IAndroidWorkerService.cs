using TycoonFactoryApp.Core.Models;

namespace TycoonFactoryApp.Core.Contracts.DataServices
{
    public interface IAndroidWorkerService
    {
        Task<AndroidWorkerDto> GetWorkerById(char workedId);
        Task<IEnumerable<AndroidWorkerDto>> GetAndroidWorkersByActivities();
        Task<IEnumerable<ActivityResponseDto>> GetActivitiesByWorker(char workedId);
    }
}
