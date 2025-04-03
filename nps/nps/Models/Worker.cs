using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace nps.Models;

[Table("workers")]
[Index(nameof(Email), IsUnique = true)]
public class Worker
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [Column("password")]
    [MaxLength(80)]
    public string Password { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [Column("last_name")]
    [MaxLength(80)]
    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; } = [];
}