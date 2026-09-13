using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterAlgorithmOperation(Guid id, Guid algorithmId, string operation, string valueProviderName, string inputParameter)
{
    public Guid Id { get; } = id;
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public string Operation { get; } = Guard.AgainstEmpty(operation);
    public string ValueProviderName { get; } = Guard.AgainstEmpty(valueProviderName);
    public string InputParameter { get; } = Guard.AgainstEmpty(inputParameter);
}

public class RegisterAlgorithmOperationParticipant(IEventStore eventStore) : IParticipant<RegisterAlgorithmOperation>
{
    public async Task HandleAsync(RegisterAlgorithmOperation message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var algorithm = new Algorithm();
        var stream = await eventStore.GetAsync(message.AlgorithmId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(algorithm);
        algorithm.Id = message.AlgorithmId;

        if (algorithm.Removed)
        {
            return;
        }

        var id = message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id;

        stream.Add(algorithm.RegisterOperation(id, message.Operation, message.ValueProviderName, message.InputParameter));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
