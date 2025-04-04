using System.ComponentModel.DataAnnotations;

namespace nps.Models.DTOS
{
    public record LoginInfo
    {
		[Required(ErrorMessage = "Email is required")]
		[RegularExpression( @"^(?=.{1,64}@)[\p{L}0-9_-]+(\.[\p{L}0-9_-]+)*@[^-][\p{L}0-9-]+(\.[\p{L}0-9-]+)*(\.[\p{L}]{2,})$", 
							ErrorMessage = "Please enter a valid email address")]
		public string Email { get; init; } = string.Empty;

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; init; } = string.Empty;
    }
}
