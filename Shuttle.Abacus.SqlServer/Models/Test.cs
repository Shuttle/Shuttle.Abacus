using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(Test), Schema = "abacus")]
[Index(nameof(Name), IsUnique = true, Name = $"UX_{nameof(Test)}_{nameof(Name)}")]
public class Test
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid AlgorithmId { get; set; }

    [Required]
    [StringLength(500)]
    public string ExpectedResult { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string ExpectedResultDataTypeName { get; set; } = "Decimal";

    [Required]
    [StringLength(10)]
    public string Comparison { get; set; } = "==";

    public ICollection<TestArgument> Arguments { get; set; } = [];
}
