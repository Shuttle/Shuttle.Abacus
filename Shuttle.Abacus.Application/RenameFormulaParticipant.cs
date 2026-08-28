using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RenameFormula(Guid formulaId, string name)
{
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
    public string Name { get; } = Guard.AgainstEmpty(name);
}

public class RenameFormulaParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RenameFormula>
{
    public async Task HandleAsync(RenameFormula message, CancellationToken cancellationToken = default)
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

        if (formula.IsNamed(message.Name))
        {
            return;
        }

        var key = Formula.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.RekeyAsync(Formula.Key(formula.Name), key, cancellationToken);

        stream.Add(formula.Rename(message.Name));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
