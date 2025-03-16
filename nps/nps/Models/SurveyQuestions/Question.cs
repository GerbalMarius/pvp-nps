using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

[Table("questions")]
public sealed class Question
{
    [Key, Column("question_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("body", TypeName = "TEXT")]
    public string Body { get; set; } = string.Empty;
    
    [Column("q_type")] 
    public long QuestionTypeId { get; set; }

    public QuestionType Type { get; set; }

    [Column("survey_id")]
    public long SurveyId { get; set; }
    
    public Survey Survey { get; set; }
}