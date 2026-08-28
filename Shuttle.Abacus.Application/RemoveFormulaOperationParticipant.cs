using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveFormulaOperation(Guid formulaId, Guid operationId)
{
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
    public Guid OperationId { get; } = Guard.AgainstEmpty(operationId);
}

public class RemoveFormulaOperationParticipant(IEventStore eventStore) : IParticipant<RemoveFormulaOperation>
{
    public async Task HandleAsync(RemoveFormulaOperation message, CancellationToken cancellationToken = default)
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

        if (formula.Removed || !formula.ContainsOperation(message.OperationId))
        {
            return;
        }

        stream.Add(formula.RemoveOperation(message.OperationId));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
