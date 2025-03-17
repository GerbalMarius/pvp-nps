using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace nps.Models;

//telephone LT - ^\+370[6-9][0-9]{7}$

[Table("users")]
[Index(nameof(Email), IsUnique = true)]//unique constraint on emails , faster search also.
public sealed class User
{
    [Key, Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required, Column("email")]
    [MaxLength(100)]
    public required string Email { get; set; }
    
    [Required, Column("password")]
    [MaxLength(80)]
    public required string Password { get; set; }
    
    [Required, Column("telephone_number")]
    [MaxLength(20)]
    public required string TelephoneNumber { get; set; }
    
    public ICollection<Order> Orders { get; set; } = [];

    public ICollection<Role> Roles { get; set; } = [];


}