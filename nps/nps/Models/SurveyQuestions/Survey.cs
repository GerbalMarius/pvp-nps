using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

[Table("surveys")]
public sealed class Survey
{
    [Key, Column("survey_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("survey_name")]
    [Required, MaxLength(100)]
    public required string Name { get; set; }
    

    [Column("created_at"), Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Order> Orders { get; set; } = [];

    public ICollection<Question> Questions { get; set; } = [];
}