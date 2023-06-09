namespace TycoonFactoryApp.Domain
{
    public class AndroidWorker
    {
        public int Id { get; set; }
        public char Name { get; set; }
        public List<ActivityWorker> Activities { get; set; }

        public AndroidWorker()
        {
            Activities = new List<ActivityWorker>();
        }
    }
}
