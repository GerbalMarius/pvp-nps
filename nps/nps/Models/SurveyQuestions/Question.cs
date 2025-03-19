using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

[Table("questions")]
public abstract class Question
{
    [Key, Column("question_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("question_text"), Required]
    [MaxLength(120)]
    public string? QuestionText { get; set; }
    
    [Column("survey_id"), Required]
    public long SurveyId { get; set; }

    public Survey Survey { get; set; } 
}