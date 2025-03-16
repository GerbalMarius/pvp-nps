using System.ComponentModel.DataAnnotations;

namespace nps.Models
{
    public record LoginInfo
    {
		[Required(ErrorMessage = "Email is required")]
		[RegularExpression( "^[_A-Za-z0-9-+]+(.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(.[A-Za-z0-9]+)*([A-Za-z]{2,})$", 
							ErrorMessage = "Please enter a valid email address")]
		public string Email { get; init; } = string.Empty;

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; init; } = string.Empty;
    }
}
