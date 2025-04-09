namespace nps.Models.DTOS;

public record QuestionDto
{
    public bool UseExisting { get; init; } = false;
    
    public long? ExistingQuestionId { get; init; }

    
    public string? QuestionText { get; init; }

    public QuestionType? Type { get; init; }

    public int? MaxOptions { get; init; }
    
    public string Choices { get; init; } = string.Empty;

    public List<string> ParsedChoices =>
    [
        ..Choices.Split(',').SelectIfPossible(str => !string.IsNullOrEmpty(str), str => str.Trim())
    ];
}