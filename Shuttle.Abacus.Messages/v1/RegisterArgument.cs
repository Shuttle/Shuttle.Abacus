namespace Shuttle.Abacus.Messages.v1;

public class RegisterArgument
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DataTypeName { get; set; } = string.Empty;
}
