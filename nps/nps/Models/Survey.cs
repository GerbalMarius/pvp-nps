using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace nps.Models;

[Table("surveys")]
public sealed class Survey
{
    [Key, Column("survey_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("final_score")]
    public int? FinalScore { get; set; }

    public ICollection<SurveyQuestions.Question> Questions { get; set; } = [];
}