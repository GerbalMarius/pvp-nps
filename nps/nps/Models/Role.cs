using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models;

[Table("roles")]
public sealed class Role
{
    [Key, Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required, Column("name")]
    [MaxLength(60)]
    public required string Name { get; set; }

    public ICollection<User> Users { get; set; } = [];

}