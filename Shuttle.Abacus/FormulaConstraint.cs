using Shuttle.Contract;

namespace Shuttle.Abacus;

public class FormulaConstraint
{
    public FormulaConstraint(Guid id, Guid argumentId, string comparison, string value)
    {
        Guard.AgainstEmpty(comparison);
        Guard.AgainstEmpty(value);

        Id = id;
        ArgumentId = argumentId;
        Comparison = comparison;
        Value = value;
    }

    public Guid Id { get; }
    public Guid ArgumentId { get; }
    public string Comparison { get; }
    public string Value { get; }
}
