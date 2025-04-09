namespace nps.Models.DTOS;

public record SurveyCreationDto
{
    public string? SurveyName { get; init; }

    public DateTime CreatedAt { get; init; }
    
    
};