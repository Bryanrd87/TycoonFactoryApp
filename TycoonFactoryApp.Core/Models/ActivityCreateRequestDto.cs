using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.Models
{
    public class ActivityCreateRequestDto
    {
        public ActivityType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<char> Workers { get; set; }

        public ActivityCreateRequestDto()
        {
            Workers = new List<char>();
        }
    }
}
