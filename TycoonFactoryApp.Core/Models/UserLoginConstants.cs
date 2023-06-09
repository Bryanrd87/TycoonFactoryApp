namespace TycoonFactoryApp.Core.Models
{
    public class UserLoginConstants
    {
        public static List<UserLoginDto> Users = new()
        {
            new UserLoginDto(){ Username="user1",Password="user1",Role="Admin"}
        };
    }
}
