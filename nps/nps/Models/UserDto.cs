namespace nps.Models;

public record UserDto
{
    public long Id { get; init; }

    public string? Email { get; init; }
    
    public string? TelephoneNumber { get; init; }
}