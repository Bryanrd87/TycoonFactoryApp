using TycoonFactoryApp.Core.Models;

namespace TycoonFactoryApp.Core.Contracts.DataServices
{
    public interface IActivityService
    {
        Task<int> CreateActivity(ActivityCreateRequestDto request);
        Task DeleteActivity(int id);
        Task UpdateActivity(ActivityUpdateRequestDto activity);
        Task<ActivityResponseDto> GetActivityById(int id);
        Task<IEnumerable<ActivityResponseDto>> GetActivities();
    }
}
