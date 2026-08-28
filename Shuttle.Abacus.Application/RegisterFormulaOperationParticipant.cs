using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterFormulaOperation(Guid id, Guid formulaId, string operation, string valueProviderName, string inputParameter)
{
    public Guid Id { get; } = id;
    public Guid FormulaId { get; } = Guard.AgainstEmpty(formulaId);
    public string Operation { get; } = Guard.AgainstEmpty(operation);
    public string ValueProviderName { get; } = Guard.AgainstEmpty(valueProviderName);
    public string InputParameter { get; } = Guard.AgainstEmpty(inputParameter);
}

public class RegisterFormulaOperationParticipant(IEventStore eventStore) : IParticipant<RegisterFormulaOperation>
{
    public async Task HandleAsync(RegisterFormulaOperation message, CancellationToken cancellationToken = default)
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

        stream.Add(formula.RegisterOperation(id, message.Operation, message.ValueProviderName, message.InputParameter));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
