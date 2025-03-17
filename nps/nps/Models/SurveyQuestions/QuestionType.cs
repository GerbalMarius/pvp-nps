using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

/// <summary>
/// Used for better determination as to what kind of html input fields to use.
/// For example : if type is short answer - use input box, if type multiple - use check boxes, etc.
/// </summary>
[Table("question_types")]
public sealed class QuestionType
{
    [Key, Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required, Column("name")]
    [MaxLength(70)]
    public string Name { get; set; }
    
    
}