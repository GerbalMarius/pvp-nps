using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models;

[Table("response_options")]
public sealed class ResponseOption
{
    [Key, Column("response_option_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("response_id"), Required]
    public long ResponseId { get; set; }
    
    public Response Response { get; set; }

    [Column("option_body")]
    [Required, MaxLength(100)]
    public required string OptionText { get; set; }
}