using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nps.Models.SurveyQuestions;

namespace nps.Models;

[Table("responses")]
public class Response
{
    [Key, Column("response_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("question_id")]
    public long QuestionId { get; set; }

    public Question Question { get; set; }  
    
    [Column("answer"), MaxLength(100)]
    public string? TextAnswer { get; set; }
    
    [Column("number_rating")]
    public int? NumberRating { get; set; }

    public ICollection<ResponseOption> ResponseOptions { get; set; } = [];

}