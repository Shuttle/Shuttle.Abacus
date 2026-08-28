using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveFormulaConstraint(Guid formulaId, Guid constraintId)
{
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
    public Guid ConstraintId { get; } = Guard.AgainstEmpty(constraintId);
}

public class RemoveFormulaConstraintParticipant(IEventStore eventStore) : IParticipant<RemoveFormulaConstraint>
{
    public async Task HandleAsync(RemoveFormulaConstraint message, CancellationToken cancellationToken = default)
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

        if (formula.Removed || !formula.ContainsConstraint(message.ConstraintId))
        {
            return;
        }

        stream.Add(formula.RemoveConstraint(message.ConstraintId));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
