using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer.Models;

[Table(nameof(ArgumentValue), Schema = "abacus")]
[PrimaryKey(nameof(ArgumentId), nameof(Value))]
public class ArgumentValue
{
    public Guid ArgumentId { get; set; }

    [StringLength(500)]
    public string Value { get; set; } = string.Empty;

    public Argument? Argument { get; set; }
}
