namespace TycoonFactoryApp.Domain
{
    public class ActivityWorker
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int WorkerId { get; set; }
        public AndroidWorker Worker { get; set; }
    }

}
