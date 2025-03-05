namespace nps.Models
{
    //Better variant would be to use immutable dataclass for transfer , because dtos don't model any real world data,
    //Making them mutable could cause undefined behavior inbetween transfers if more than one user performs the same action.
    //Easiest way to do this is to create records or classes with get; init accessors.
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
