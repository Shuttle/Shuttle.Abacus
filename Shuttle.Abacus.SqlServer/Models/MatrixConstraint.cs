using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(MatrixConstraint), Schema = "abacus")]
[PrimaryKey(nameof(MatrixId), nameof(Axis), nameof(Index))]
[Index(nameof(Id), IsUnique = true, Name = $"UX_{nameof(MatrixConstraint)}_{nameof(Id)}")]
public class MatrixConstraint
{
    public Guid Id { get; set; }
    public Guid MatrixId { get; set; }

    [StringLength(10)]
    public string Axis { get; set; } = string.Empty;

    public int Index { get; set; }

    [Required]
    [StringLength(10)]
    public string Comparison { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;

    public Matrix? Matrix { get; set; }
}
