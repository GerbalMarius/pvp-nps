using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

public class SingleChoiceQuestion : Question
{
    [Column("max_options")]
    public int MaxOptions { get; set; } = 2;
}