using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(Matrix), Schema = "abacus")]
[Index(nameof(Name), IsUnique = true, Name = $"UX_{nameof(Matrix)}_{nameof(Name)}")]
public class Matrix
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid RowArgumentId { get; set; }
    public Guid? ColumnArgumentId { get; set; }

    [Required]
    [StringLength(50)]
    public string DataTypeName { get; set; } = "Text";

    public ICollection<MatrixConstraint> Constraints { get; set; } = [];
    public ICollection<MatrixElement> Elements { get; set; } = [];
}
