using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(Argument), Schema = "abacus")]
[Index(nameof(Name), IsUnique = true, Name = $"UX_{nameof(Argument)}_{nameof(Name)}")]
public class Argument
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string DataTypeName { get; set; } = string.Empty;

    public ICollection<ArgumentValue> Values { get; set; } = [];
}
