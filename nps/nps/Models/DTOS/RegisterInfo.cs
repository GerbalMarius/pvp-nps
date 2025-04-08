using System.ComponentModel.DataAnnotations;

namespace nps.Models.DTOS;

public record RegisterInfo : LoginInfo
{
    [Required(ErrorMessage = "Name is required.")]
    [RegularExpression("(^[A-Z])([a-z]+)", ErrorMessage = "Name must start with an uppercase letter")]
    public string Name { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required.")]
    [RegularExpression("(^[A-Z])([a-z]+)", ErrorMessage = "Last name must start with an uppercase letter")]
    public string LastName { get; init; } = string.Empty;
};