using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace nps.Models;

[Table("workers")]
[Index(nameof(Email), IsUnique = true)]
public sealed class Worker
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("email")]
    [MaxLength(100)]
    public required string Email { get; set; }

    [Required]
    [Column("password")]
    [MaxLength(80)]
    public required string Password { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [Column("last_name")]
    [MaxLength(80)]
    public required string LastName { get; set; }

    public ICollection<Role> Roles { get; set; } = [];

    public ICollection<Order> Orders { get; set; } = [];
}