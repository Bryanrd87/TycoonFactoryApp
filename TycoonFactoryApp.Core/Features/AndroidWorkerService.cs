using AutoMapper;
using TycoonFactoryApp.Core.Contracts.DataServices;
using TycoonFactoryApp.Core.Contracts.Persistence;
using TycoonFactoryApp.Core.Models;

namespace TycoonFactoryApp.Core.Features
{
    public class AndroidWorkerService : IAndroidWorkerService
    {
        private readonly IAndroidWorkerRepository _androidWorkerRepository;
        private readonly IMapper _mapper;

        public AndroidWorkerService(IAndroidWorkerRepository androidWorkerRepository, IMapper mapper)
        {
            _androidWorkerRepository = androidWorkerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ActivityResponseDto>> GetActivitiesByWorker(char workedId)
        {
            var result = await _androidWorkerRepository.GetActivitiesByWorkerIdAsync(workedId);
            return _mapper.Map<IEnumerable<ActivityResponseDto>>(result);
        }

        public async Task<IEnumerable<AndroidWorkerDto>> GetAndroidWorkersByActivities()
        {          
            var result = await _androidWorkerRepository.GetAndroidWorkersByActivities();
            var response = result.GroupBy(w => w.Name)
                            .Select(g => new AndroidWorkerDto
                            {
                                Id = g.Key,
                                Activities = g.SelectMany(w => w.Activities)
                                    .Select(a => new ActivityResponseDto
                                    {
                                        Id = a.Activity.Id,
                                        Type = a.Activity.ActivityType,
                                        StartDate = a.Activity.StartDate,
                                        EndDate = a.Activity.EndDate                                        
                                    }).ToList()
                        }).ToList();            
            return response;
        }

        public Task<AndroidWorkerDto> GetWorkerById(char workedId)
        {
            throw new NotImplementedException();
        }
    }
}
