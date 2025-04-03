using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace nps.Models;

[Table("roles")]
[Index(nameof(Name), IsUnique = true)]
public sealed class Role
{
    [Key, Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required, Column("name")]
    [MaxLength(60)]
    public string? Name { get; set; }

    public ICollection<Worker> Workers { get; set; } = [];

}