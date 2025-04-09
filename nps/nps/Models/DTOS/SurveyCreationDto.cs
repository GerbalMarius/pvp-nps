using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nps.Models.DTOS;

public record SurveyCreationDto
{
    [MaxLength(100)]
    public string? Name { get; init; }

    public List<QuestionDto> Questions { get; init; } = [];

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine($"{Name}:");
        
        sb.AppendJoin(Environment.NewLine, Questions);
        sb.AppendLine();
        return sb.ToString();
    }
};