namespace TycoonFactoryApp.Core.Models
{
    public class ActivityUpdateRequestDto
    {
        public int Id { get; set; }     
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }  
        public static ActivityUpdateRequestDto Empty => new()
        {
            Id = 0,
            StartDate = default,
            EndDate = default          
        };
    }
}
