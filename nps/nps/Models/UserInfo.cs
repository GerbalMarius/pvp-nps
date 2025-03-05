using System.ComponentModel.DataAnnotations;

namespace nps.Models
{
    //Better variant would be to use immutable dataclass for transfer , because dtos don't model any real world data,
    //Making them mutable could cause undefined behavior inbetween transfers if more than one user performs the same action.
    //Easiest way to do this is to create records or classes with get; init accessors.
    public class UserInfo
    {
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public UserInfo()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
