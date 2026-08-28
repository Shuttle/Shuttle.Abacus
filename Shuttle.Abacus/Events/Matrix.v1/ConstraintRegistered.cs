namespace Shuttle.Abacus.Events.Matrix.v1;

public class ConstraintRegistered
{
    public Guid Id { get; set; }
    public string Axis { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
