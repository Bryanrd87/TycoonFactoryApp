using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.Models
{
    public class ActivityResponseDto
    {
        public int Id { get; set; }
        public ActivityType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<char> Workers { get; set; }

        public ActivityResponseDto()
        {
            Workers = new List<char>();
        }

        public static ActivityResponseDto Empty => new()
        {
            Id = 0,
            StartDate = default,
            EndDate = default,
            Type = default,
            Workers = new List<char>()
        };
    }
}
