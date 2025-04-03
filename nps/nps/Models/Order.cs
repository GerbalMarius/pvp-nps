using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using nps.Models.SurveyQuestions;

namespace nps.Models;

[Table("orders")]
[Index(nameof(Number), IsUnique = true)]
public sealed class Order
{
    [Key, Column("order_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("order_number")]
    [MaxLength(60), Required]
    public string? Number { get; set; }
    
    [Column("order_date")]
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Column("delivery_date")]
    [Required]
    public DateTime DeliveryDate { get; set; }
    
    [Column("worker_id")]
    public long? WorkerId { get; set; }
    
    public ICollection<Survey> Surveys { get; set; }
    public Worker worker { get; set; }
}