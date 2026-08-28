namespace Shuttle.Abacus;

public class ConstraintViolation(Guid argumentId, string argumentValue, string comparison, string constraintValue)
{
    public Guid ArgumentId { get; } = argumentId;
    public string ArgumentValue { get; } = argumentValue;
    public string Comparison { get; } = comparison;
    public string ConstraintValue { get; } = constraintValue;
}
