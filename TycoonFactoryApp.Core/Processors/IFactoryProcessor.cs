using TycoonFactoryApp.Core.Models;

namespace TycoonFactoryApp.Core.Processors
{
    public interface IFactoryProcessor
    {
        Task<ActivityCreationResultDto> CreateActivityAsync(ActivityCreateRequestDto request);        
        Task<ActivityDeletionResultDto> DeleteActivityAsync(int id);
        Task<ActivityUpdateResultDto> UpdateActivityAsync(ActivityUpdateRequestDto modifiedRequest);
        Task<List<AndroidWorkerTop10Dto>> GetTop10AndroidWorkingAsync();
        Task<bool> IsWorkerAvailable(List<char> androidWorkers, DateTime startDate, DateTime endDate, int activityId = 0);
        Task<IEnumerable<ActivityResponseDto>> GetAllActivitiesAsync();
        Task<ActivityResultDto> GetActivityByIdAsync(int id);
    }
}
