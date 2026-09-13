using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(AlgorithmConstraint), Schema = "abacus")]
public class AlgorithmConstraint
{
    [Key]
    public Guid Id { get; set; }

    public Guid AlgorithmId { get; set; }
    public Guid ArgumentId { get; set; }

    [Required]
    [StringLength(10)]
    public string Comparison { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;

    public Algorithm? Algorithm { get; set; }
}
