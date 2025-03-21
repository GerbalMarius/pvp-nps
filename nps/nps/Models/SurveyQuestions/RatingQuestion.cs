using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;


[Table("rating_questions")]
public class RatingQuestion : Question
{
    [Column("min_value")]
    public int MinValue { get; set; } = 0;
    [Column("max_value")]
    public int MaxValue { get; set; } = 10;
}