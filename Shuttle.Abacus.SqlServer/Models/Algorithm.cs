using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(Algorithm), Schema = "abacus")]
[Index(nameof(Name), IsUnique = true, Name = $"UX_{nameof(Algorithm)}_{nameof(Name)}")]
public class Algorithm
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string? MaximumAlgorithmName { get; set; }

    [StringLength(200)]
    public string? MinimumAlgorithmName { get; set; }

    public ICollection<AlgorithmOperation> Operations { get; set; } = [];
    public ICollection<AlgorithmConstraint> Constraints { get; set; } = [];
}
