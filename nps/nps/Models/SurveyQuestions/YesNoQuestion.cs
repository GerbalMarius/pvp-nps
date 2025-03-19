using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

public class YesNoQuestion : Question
{
    [Column("is_positive")]
    public bool IsPositive { get; set; }
}