using System.ComponentModel.DataAnnotations;

namespace nps.Models
{
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
