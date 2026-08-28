namespace Shuttle.Abacus.Messages.v1;

public class ArgumentValueAdded
{
    public Guid ArgumentId { get; set; }
    public string Value { get; set; } = string.Empty;
}
