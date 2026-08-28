using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RemoveFormula(Guid formulaId)
{
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
}

public class RemoveFormulaParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RemoveFormula>
{
    public async Task HandleAsync(RemoveFormula message, CancellationToken cancellationToken = default)
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

        stream.Add(formula.Remove());

        await idKeyRepository.RemoveAsync(message.FormulaId, cancellationToken);

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
