using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models;

[Table("orders")]
public sealed class Order
{
    [Key, Column("order_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("order_date")]
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Column("delivery_date")]
    [Required]
    public DateTime DeliveryDate { get; set; }
    
    [Column("user_id")]
    [Required]
    public long UserId { get; set; }
    
    
    public User User { get; set; }
    
}