using TycoonFactoryApp.Core.Contracts.DataServices;
using TycoonFactoryApp.Core.Models;

namespace TycoonFactoryApp.Core.Processors
{
    public class FactoryProcessor : IFactoryProcessor
    {
        private readonly IActivityService _activityService;
        private readonly IAndroidWorkerService _androidWorkerService;
        private readonly ActivityCreationResultDto activityCreationResult;
        private readonly ActivityDeletionResultDto activityDeletionResult;
        private readonly ActivityUpdateResultDto activityUpdateResult;
        private readonly ActivityResultDto activityResultDto;

        public FactoryProcessor(IActivityService activityService, IAndroidWorkerService androidWorkerService)
        {
            _activityService = activityService;
            _androidWorkerService = androidWorkerService;            
            activityCreationResult = new();
            activityDeletionResult = new();
            activityUpdateResult = new();
            activityResultDto = new();
        }

        public async Task<ActivityCreationResultDto> CreateActivityAsync(ActivityCreateRequestDto request)
        {
            if (request is null)
            {
                activityCreationResult.Activity = ActivityResponseDto.Empty;
                activityCreationResult.Message = "Empty request";
                return activityCreationResult;
            }
            var isWorkerAvailable = await IsWorkerAvailable(request.Workers, request.StartDate, request.EndDate);
            if (!isWorkerAvailable)
            {
                activityCreationResult.Activity = ActivityResponseDto.Empty;
                activityCreationResult.Message = "Selected android workers are not available during the specified time period.";
                return activityCreationResult;
            }

            var result = await _activityService.CreateActivity(request);

            activityCreationResult.Activity = new ActivityResponseDto { Id = result, EndDate = request.EndDate, StartDate = request.StartDate, Type = request.Type, Workers = request.Workers };
            return activityCreationResult;
        }

        public async Task<ActivityDeletionResultDto> DeleteActivityAsync(int id)
        {
            var activity = await _activityService.GetActivityById(id);

            if (activity == null || activity.Id == 0)
            {
                activityDeletionResult.Message = $"Activity with Id {id} Not Found";
                return activityDeletionResult;
            }
            await _activityService.DeleteActivity(id);

            activityDeletionResult.IsSuccess = true;
            return activityDeletionResult;
        }
        public async Task<ActivityUpdateResultDto> UpdateActivityAsync(ActivityUpdateRequestDto modifiedRequest)
        {
            var activity = await _activityService.GetActivityById(modifiedRequest.Id);
            if (activity == null)
            {
                activityUpdateResult.Activity = ActivityResponseDto.Empty;
                activityUpdateResult.Message = $"Activity with Id {modifiedRequest.Id} Not Found";
                return activityUpdateResult;
            }
            var isWorkerAvailable = await IsWorkerAvailable((List<char>)activity.Workers, modifiedRequest.StartDate, modifiedRequest.EndDate, modifiedRequest.Id);
            if (!isWorkerAvailable)
            {
                activityUpdateResult.Activity = ActivityResponseDto.Empty;
                activityUpdateResult.Message = "Selected android workers are not available during the specified time period.";
                return activityUpdateResult;
            }
            activity.StartDate = modifiedRequest.StartDate;
            activity.EndDate = modifiedRequest.EndDate;

            await _activityService.UpdateActivity(modifiedRequest);

            activityUpdateResult.Activity = activity;
            return activityUpdateResult;
        }

        public async Task<List<AndroidWorkerTop10Dto>> GetTop10AndroidWorkingAsync()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(7);
            var androidWorker = await _androidWorkerService.GetAndroidWorkersByActivities();
            var androidWorkingTimes = new List<AndroidWorkerTop10Dto>();

            foreach (var worker in androidWorker)
            {
                var activitiesInRange = worker.Activities
                                              .Where(activity => activity.StartDate >= startDate && activity.EndDate <= endDate);

                var totalTicks = activitiesInRange
                    .Select(activity => activity.EndDate.Ticks - activity.StartDate.Ticks)
                    .Sum();
                var totalWorkingTime = TimeSpan.FromTicks(totalTicks);
                androidWorkingTimes.Add(new AndroidWorkerTop10Dto { ActivityTotalDuration = totalWorkingTime.ToString("d'd 'h'h 'm'm 's's'"), Id = worker.Id });
            }

            return androidWorkingTimes.OrderByDescending(pair => pair.ActivityTotalDuration).Take(10).ToList();
        }

        public async Task<bool> IsWorkerAvailable(List<char> androidWorkers, DateTime startDate, DateTime endDate, int activityId = 0)
        {
            foreach (var androidWorker in androidWorkers)
            {
                var activities = await _androidWorkerService.GetActivitiesByWorker(androidWorker);

                if (activities != null)
                {
                    activities = activityId > 0 ? activities.Where(q=> q.Id != activityId) : activities;
                    foreach (var activity in activities)
                    {
                        var newEndDate = activity.EndDate.AddHours(activity.Type == Domain.ActivityType.BuildMachine ? 4 : 2);
                        if (endDate >= activity.StartDate && startDate <= newEndDate)
                            return false;
                    }
                }
            }
            return true;
        }

        public async Task<IEnumerable<ActivityResponseDto>> GetAllActivitiesAsync()
        {
            return await _activityService.GetActivities();
        }

        public async Task<ActivityResultDto> GetActivityByIdAsync(int id)
        {
            var result = await _activityService.GetActivityById(id);
            if (result.Id > 0)
            {
                activityResultDto.Activity = result;
                return activityResultDto;
            }
            activityResultDto.Message = $"Activity with Id {id} Not Found";
            return activityResultDto;
        }
    }
}
