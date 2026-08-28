using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(Formula), Schema = "abacus")]
[Index(nameof(Name), IsUnique = true, Name = $"UX_{nameof(Formula)}_{nameof(Name)}")]
public class Formula
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string? MaximumFormulaName { get; set; }

    [StringLength(200)]
    public string? MinimumFormulaName { get; set; }

    public ICollection<FormulaOperation> Operations { get; set; } = [];
    public ICollection<FormulaConstraint> Constraints { get; set; } = [];
}
