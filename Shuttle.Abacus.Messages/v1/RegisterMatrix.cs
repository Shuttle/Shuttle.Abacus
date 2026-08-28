namespace Shuttle.Abacus.Messages.v1;

public class RegisterMatrix
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RowArgumentId { get; set; }
    public Guid? ColumnArgumentId { get; set; }
    public string DataTypeName { get; set; } = string.Empty;
}
