namespace Shuttle.Abacus.Messages.v1;

public class RegisterMatrixConstraint
{
    public Guid Id { get; set; }
    public Guid MatrixId { get; set; }
    public string Axis { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
