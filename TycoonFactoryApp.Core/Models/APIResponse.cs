using System.Net;

namespace TycoonFactoryApp.Core.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
        public bool IsSuccess { get; set; } = false;    
        public List<string> ErrorMessage { get; set; }
        public object? Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
