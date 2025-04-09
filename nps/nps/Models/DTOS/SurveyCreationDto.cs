using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nps.Models.DTOS;

public record SurveyCreationDto
{
    [MaxLength(100)]
    [Required(ErrorMessage = "Survey name is required")]
    public string Name { get; init; } = string.Empty;

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