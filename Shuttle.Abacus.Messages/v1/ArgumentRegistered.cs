namespace Shuttle.Abacus.Messages.v1;

public class ArgumentRegistered
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DataTypeName { get; set; } = string.Empty;
}
