using System.ComponentModel;

namespace TycoonFactoryApp.Core.Models
{
    public class UserLoginRequestDto
    {
        [DefaultValue("user1")]
        public string Username { get; set; } = string.Empty;
        [DefaultValue("user1")]
        public string Password { get; set; } = string.Empty;
        [DefaultValue("Admin")]
        public string Role { get; set; } = string.Empty;
    }
}
