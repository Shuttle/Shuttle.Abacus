using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(FormulaOperation), Schema = "abacus")]
[PrimaryKey(nameof(FormulaId), nameof(SequenceNumber))]
[Index(nameof(Id), IsUnique = true, Name = $"UX_{nameof(FormulaOperation)}_{nameof(Id)}")]
public class FormulaOperation
{
    public Guid Id { get; set; }
    public Guid FormulaId { get; set; }
    public int SequenceNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string Operation { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string ValueProviderName { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string InputParameter { get; set; } = string.Empty;

    public Formula? Formula { get; set; }
}
