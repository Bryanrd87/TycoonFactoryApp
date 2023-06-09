using TycoonFactoryApp.Core.Contracts.DataServices;
using TycoonFactoryApp.Core.Contracts.Persistence;
using TycoonFactoryApp.Core.Models;
using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.Features
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task<int> CreateActivity(ActivityCreateRequestDto request)
        {
            var activity = new Activity()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ActivityType = request.Type
            };
            foreach (var workerId in request.Workers)
            {
                var worker1 = new AndroidWorker { Name = workerId };

                activity.AndroidWorkers.Add(new ActivityWorker { Worker = worker1 });
            }

            var result = await _activityRepository.AddAsync(activity);
            return result.Id;
        }

        public async Task DeleteActivity(int id)
        {
            await _activityRepository.RemoveActivity(id);
        }

        public async Task<IEnumerable<ActivityResponseDto>> GetActivities()
        {
            var activities = await _activityRepository.GetAllAsync();
            var response = activities.Select(a => new ActivityResponseDto
            {
                Id = a.Id,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                Type = a.ActivityType,
                Workers = a.AndroidWorkers.Select(q => q.Worker.Name).ToList()
            }).ToList();
            return response;
        }

        public async Task<ActivityResponseDto> GetActivityById(int id)
        {
            var response = new ActivityResponseDto();
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity != null)
            {
                response = new ActivityResponseDto
                {
                    Id = activity.Id,
                    EndDate = activity.EndDate,
                    StartDate = activity.StartDate,
                    Type = activity.ActivityType,
                    Workers = activity.AndroidWorkers.Select(q => q.Worker.Name).ToList()
                };
            }
            return response;
        }

        public async Task UpdateActivity(ActivityUpdateRequestDto activity)
        {
            var activityToUpdate = await _activityRepository.GetByIdAsync(activity.Id);
            activityToUpdate.EndDate = activity.EndDate;
            activityToUpdate.StartDate = activity.StartDate;
            await _activityRepository.Update(activityToUpdate);
        }
    }
}
