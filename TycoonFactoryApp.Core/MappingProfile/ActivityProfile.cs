using AutoMapper;
using TycoonFactoryApp.Core.Models;
using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.MappingProfile
{
    public class ActivityProfile:Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityResponseDto>().ReverseMap();
            CreateMap<Activity, ActivityCreateRequestDto>().ReverseMap();            
        }
    }
}
