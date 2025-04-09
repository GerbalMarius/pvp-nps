using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nps.Models.SurveyQuestions;

namespace nps.Models;

[Table("responses")]
public sealed class Response
{
    [Key, Column("response_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("question_id")]
    public required long QuestionId { get; set; }

    public Question Question { get; set; }  
    
    [Column("answer"), MaxLength(100)]
    public string? TextAnswer { get; set; }
    
    [Column("number_rating")]
    public int? NumberRating { get; set; }

    [Column("response_date")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? ResponseDate { get; set; }

    public ICollection<ResponseOption> ResponseOptions { get; set; } = [];
}