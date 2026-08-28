using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(MatrixElement), Schema = "abacus")]
[PrimaryKey(nameof(MatrixId), nameof(Row), nameof(Column))]
[Index(nameof(Id), IsUnique = true, Name = $"UX_{nameof(MatrixElement)}_{nameof(Id)}")]
public class MatrixElement
{
    public Guid Id { get; set; }
    public Guid MatrixId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;

    public Matrix? Matrix { get; set; }
}
