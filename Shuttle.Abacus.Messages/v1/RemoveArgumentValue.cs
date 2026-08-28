namespace Shuttle.Abacus.Messages.v1;

public class RemoveArgumentValue
{
    public Guid ArgumentId { get; set; }
    public string Value { get; set; } = string.Empty;
}
