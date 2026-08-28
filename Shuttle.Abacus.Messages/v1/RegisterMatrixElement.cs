namespace Shuttle.Abacus.Messages.v1;

public class RegisterMatrixElement
{
    public Guid Id { get; set; }
    public Guid MatrixId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public string Value { get; set; } = string.Empty;
}
