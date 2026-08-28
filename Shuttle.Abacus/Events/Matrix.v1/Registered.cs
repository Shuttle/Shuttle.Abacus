namespace Shuttle.Abacus.Events.Matrix.v1;

public class Registered
{
    public string Name { get; set; } = string.Empty;
    public Guid RowArgumentId { get; set; }
    public Guid? ColumnArgumentId { get; set; }
    public string DataTypeName { get; set; } = string.Empty;
}
