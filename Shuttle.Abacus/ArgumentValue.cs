namespace Shuttle.Abacus;

public class ArgumentValue(Guid id, string value)
{
    public Guid Id { get; } = id;
    public string Value { get; } = value;
}
