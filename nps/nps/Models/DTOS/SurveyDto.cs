using System.Text;

namespace nps.Models.DTOS;

public record SurveyDto
{
    public Dictionary<long, List<string>> CheckBoxAnswers { get; } = [];
    
    public Dictionary<long, string> SingleChoiceAnswers { get; } = [];

    public Dictionary<long, string> DropDownAnswers { get; } = [];

    public Dictionary<long, int> RatingAnswers { get; } = [];

    public Dictionary<long, string> TextAnswers { get; } = [];
    
    //for logging purposes , mostly.
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("CheckBoxAnswers:");
        foreach (var (key, value) in CheckBoxAnswers)
        {
            sb.AppendLine($"  Question {key}: [{string.Join(", ", value)}]");
        }

        sb.AppendLine("SingleChoiceAnswers:");
        foreach (var (key, value) in SingleChoiceAnswers)
        {
            sb.AppendLine($"  Question {key}: {value}");
        }

        sb.AppendLine("DropDownAnswers:");
        foreach (var (key, value) in DropDownAnswers)
        {
            sb.AppendLine($"  Question {key}: {value}");
        }

        sb.AppendLine("RatingAnswers:");
        foreach (var (key, value) in RatingAnswers)
        {
            sb.AppendLine($"  Question {key}: {value}");
        }

        sb.AppendLine("TextAnswers:");
        foreach (var (key, value) in TextAnswers)
        {
            sb.AppendLine($"  Question {key}: {value}");
        }

        return sb.ToString();
    }
}