using System.ComponentModel.DataAnnotations;

namespace nps.Models
{
    public class UserInfo
    {
		[Required]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public UserInfo()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
