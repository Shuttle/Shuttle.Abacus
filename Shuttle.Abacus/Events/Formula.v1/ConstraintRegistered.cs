namespace Shuttle.Abacus.Events.Formula.v1;

public class ConstraintRegistered
{
    public Guid Id { get; set; }
    public Guid ArgumentId { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
