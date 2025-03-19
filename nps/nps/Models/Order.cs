using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace nps.Models;

[Table("orders")]
[Index(nameof(Number), IsUnique = true)]
public sealed class Order
{
    [Key, Column("order_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("order_number")]
    [MaxLength(100), Required]
    public string? Number { get; set; }
    
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