using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterFormulaConstraint(Guid id, Guid formulaId, Guid argumentId, string comparison, string value)
{
    public Guid Id { get; } = id;
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
    public string Comparison { get; } = Guard.AgainstEmpty(comparison);
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RegisterFormulaConstraintParticipant(IEventStore eventStore) : IParticipant<RegisterFormulaConstraint>
{
    public async Task HandleAsync(RegisterFormulaConstraint message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var formula = new Formula();
        var stream = await eventStore.GetAsync(message.FormulaId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(formula);
        formula.Id = message.FormulaId;

        if (formula.Removed)
        {
            return;
        }

        var id = message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id;

        stream.Add(formula.RegisterConstraint(id, message.ArgumentId, message.Comparison, message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
