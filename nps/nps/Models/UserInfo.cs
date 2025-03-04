namespace nps.Models
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserInfo()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
