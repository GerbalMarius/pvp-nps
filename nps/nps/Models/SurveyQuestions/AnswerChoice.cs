using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

[Table("answer_choices")]
public sealed class AnswerChoice
{
    [Key, Column("answer_choice_id")]
    public long Id { get; set; }

    [Column("question_id"), Required]
    public long QuestionId { get; set; }

    public Question Question { get; set; }

    [Column("answer_text"), Required]
    [MaxLength(100)]
    public string Text { get; set; }
}