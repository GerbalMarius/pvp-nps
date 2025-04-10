using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;

namespace nps.Models.SurveyQuestions;

[Table("questions")]
public abstract class Question
{
    [Key, Column("question_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("question_text"), Required]
    [MaxLength(120)]
    public required string QuestionText { get; set; }

    public ICollection<Response> Responses { get; set; } = [];

    public ICollection<Survey> Surveys { get; set; } = [];
    
    public ICollection<AnswerChoice> Choices { get; set; } = [];
}