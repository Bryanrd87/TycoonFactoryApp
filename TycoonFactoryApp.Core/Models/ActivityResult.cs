namespace TycoonFactoryApp.Core.Models
{
    public abstract class ActivityResult
    {
        public ActivityResponseDto Activity { get; set; }

        protected ActivityResult()
        {
            Activity = new ActivityResponseDto();
        }

        public string Message { get; set; } = string.Empty;
    }
}
