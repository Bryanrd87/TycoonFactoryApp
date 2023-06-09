namespace TycoonFactoryApp.Domain
{
    public class Activity
    {
        public Activity()
        {
            AndroidWorkers = new List<ActivityWorker>();
        }
        public int Id { get; set; }       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }     
        public ActivityType ActivityType { get; set; }
        public List<ActivityWorker> AndroidWorkers { get; set; }        
    }
}